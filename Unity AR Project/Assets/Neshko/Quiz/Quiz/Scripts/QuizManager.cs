using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
//using UnityEngine.UIElements;
using UnityEngine.UI;


public class QuizManager : MonoBehaviour
{
    [SerializeField] private List<QuizDataScriptable> quizDataList;
    
  
    
    [SerializeField] private QuizGameUI quizUi;
    
    private float _timeInSeconds;
    public string currentCategory = "";
    private int correctAnswers = 0;

    private List<Question> _questions;
    private Question _selectedQuestion = new Question();

    private int _gameScore;

    private int remainingAttempts;
    

    private float _currentTime;
    private QuizDataScriptable _dataScriptable;


    private GameStatus _gameStatus = GameStatus.NextGame;
    StatsManager Stats_Manager;

    private void Start()
    {
        Stats_Manager = (StatsManager)FindObjectOfType(typeof(StatsManager));
    }

    public GameStatus GameStatus
    {
        get { return _gameStatus; }
    }

    public List<QuizDataScriptable> QuizData
    {
        get => quizDataList;
    }
    

    public void StartGame(int categoryIndex, string category)
    {
        currentCategory = category;
        correctAnswers = 0;
        _gameScore = 0;
        remainingAttempts = 3;

        _currentTime = _timeInSeconds;
        //set the questions data
        _questions = new List<Question>();
        _dataScriptable = quizDataList[categoryIndex];
        _questions.AddRange(_dataScriptable.questions);
        //select the question
        SelectQuestion();
        _gameStatus = GameStatus.Playing;


        if (currentCategory == "Easy")
        {
            _timeInSeconds = 30;
            _currentTime = _timeInSeconds;
        }

        if (currentCategory == "Medium")
        {
            _timeInSeconds = 25;
            _currentTime = _timeInSeconds;
        }

        if (currentCategory == "Hard")
        {
            _timeInSeconds = 20;
            _currentTime = _timeInSeconds;
        }
    }
    // Method to select a question randomly
    private void SelectQuestion()
    {
        //get the random number
        int randomQuestion = UnityEngine.Random.Range(0, _questions.Count);
        //set the selectedQuetion
        _selectedQuestion = _questions[randomQuestion];
        //send the question to quizGameUI
        quizUi.SetQuestion(_selectedQuestion);
        _questions.RemoveAt(randomQuestion);
    }

    // This Method sets score stars and score text result
   
    private void Update()
    {

        quizUi.timerImage.fillAmount = _currentTime / _timeInSeconds;
        if (_gameStatus == GameStatus.Playing)
        {
            _currentTime -= Time.deltaTime;
            SetTime(_currentTime);
        }
    }

    // Method foe setting gameplay time
    void SetTime(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(_currentTime); //set the time value
        quizUi.TimerText.text = time.ToString("mm':'ss"); //convert time to Time format

        if (_currentTime <= 0)
        {
            GameOver();
                quizUi.endGameReasonText.text = "Time's Up!";
        }

        if (_currentTime <= 5)
        {
                quizUi.TimerText.color = Color.red;
                quizUi.timerImage.color = Color.red;
        }
    }
// Method for checking if the answer is right
    public bool Answer(string selectedOption)
    {      
        bool correct = false;
        
        // checking if the selected question is the same as the right answer
        if (_selectedQuestion.correctAnswer == selectedOption)
        {
            correctAnswers++;
            correct = true;
            _gameScore += 50;
            quizUi.ScoreText.text = "Score:" + _gameScore;           

           // Displaying grading stars
        }
        else if (_selectedQuestion.correctAnswer != selectedOption)
        {
            // If the answer is wrong
            remainingAttempts--;
            quizUi.ReduceAttempts(remainingAttempts);
           // print("CorrectAnswers" +  correctAnswers);
            if (remainingAttempts == 0)
            {
                GameOver(); 
                quizUi.endGameReasonText.text = "Game Over";
            }
        }
        if (_gameStatus == GameStatus.Playing)
        {
            if (_questions.Count > 0)
            {
                //Wait until the next random question is displayed
                Invoke("SelectQuestion", 0.4f);
            } 
            else
            {
                GameOver();
            }
        }
        if (correctAnswers == 0)
        {
            quizUi.GradingText.text = "0/5";
            Stats_Manager.UpdateNumber(Convert.ToInt32(_dataScriptable.MonumentNumber), 0);

        }

        quizUi.scoreImageList[0].GetComponent<Image>().color = Color.white;
        quizUi.scoreImageList[1].GetComponent<Image>().color = Color.white;
        quizUi.scoreImageList[2].GetComponent<Image>().color = Color.white;
        quizUi.scoreImageList[3].GetComponent<Image>().color = Color.white;
        quizUi.scoreImageList[4].GetComponent<Image>().color = Color.white;

        if (correctAnswers == 1)
        {
            quizUi.GradingText.text = "1/5";
            quizUi.scoreImageList[0].GetComponent<Image>().color = Color.yellow;
            Stats_Manager.UpdateNumber(Convert.ToInt32(_dataScriptable.MonumentNumber), 1);

        }

        if (correctAnswers == 2)
        {
            quizUi.GradingText.text = "2/5";
            quizUi.scoreImageList[0].GetComponent<Image>().color = Color.yellow;
            quizUi.scoreImageList[1].GetComponent<Image>().color = Color.yellow;
            Stats_Manager.UpdateNumber(Convert.ToInt32(_dataScriptable.MonumentNumber), 2);

        }

        if (correctAnswers == 3)
        {
            quizUi.GradingText.text = "3/5";
            quizUi.scoreImageList[0].GetComponent<Image>().color = Color.yellow;
            quizUi.scoreImageList[2].GetComponent<Image>().color = Color.yellow;
            quizUi.scoreImageList[1].GetComponent<Image>().color = Color.yellow;
            Stats_Manager.UpdateNumber(Convert.ToInt32(_dataScriptable.MonumentNumber), 3);

        }
        if (correctAnswers == 4)
        {
            quizUi.GradingText.text = "4/5";
            quizUi.scoreImageList[0].GetComponent<Image>().color = Color.yellow;   
            quizUi.scoreImageList[1].GetComponent<Image>().color = Color.yellow;
            quizUi.scoreImageList[2].GetComponent<Image>().color = Color.yellow;
            quizUi.scoreImageList[3].GetComponent<Image>().color = Color.yellow;
            Stats_Manager.UpdateNumber(Convert.ToInt32(_dataScriptable.MonumentNumber), 4);

        }
        if (correctAnswers == 5)
        {
            quizUi.GradingText.text = "Excellent! 5/5";
            Stats_Manager.UpdateNumber(Convert.ToInt32(_dataScriptable.MonumentNumber), 5);
            foreach (var stars in quizUi.scoreImageList)
            {
                stars.GetComponent<Image>().color = Color.yellow; 
            } 
        }

        quizUi.Resetter();
        return correct;
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt(currentCategory, correctAnswers); // Saving final score int to PlayerPrefs
        _gameStatus = GameStatus.NextGame;
        quizUi._retryButton.interactable = true;
        StartCoroutine(FadeUI.Fade( 1,quizUi.GameOverUI,1f));
        //quizUi.GameOverPanel.SetActive(true);
        remainingAttempts = 3;
    }
}

//Class characterizing the questions (Scriptable Object)
[System.Serializable]
public class Question
{
    public string questionInfo;         //question text
    public List<string> answerOptions;        //options to select
    public string correctAnswer;           //correct option
}

[SerializeField]
public enum GameStatus
{
    Playing,
    NextGame
}
