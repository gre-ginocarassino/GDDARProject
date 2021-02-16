using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizGameUI : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager; 
    [SerializeField] private CategoryBtnScript difficultyButtonPrefab;
    [SerializeField] private GameObject scrollHolder;
    [SerializeField] private Text scoreText, timerText;
    [SerializeField] private List<Image> answerAttemptsList;
    
    [SerializeField] public List<Image> scoreImageList;
    [SerializeField] private GameObject gameOverPanel, mainMenu, gamePanel;
    [SerializeField] private Color correctCol, wrongCol, normalCol; 
    [SerializeField] private Text questionInfoText; 
    [SerializeField] private List<Button> answerButtons;

    private Question _question;          //store current question data
    private bool itWasAnswered = false;      //bool to keep track if answered or not

    public Text TimerText { get => timerText; }
    public Text ScoreText { get => scoreText; }
    public GameObject GameOverPanel { get => gameOverPanel; }//getter

    private void Start()
    {
        
        for (int i = 0; i < answerButtons.Count; i++)
        {
            Button button = answerButtons[i];
            button.onClick.AddListener(() => OnClick(button));
        }
        
        CreateDifficultyButtons();

    }

    public void SetQuestion(Question question)
    {
      
        this._question = question;
        questionInfoText.text = question.questionInfo; 

        // Here we randomize the answers to be on different option buttons
        List<string> answerOptions = RandomAnswersList.ShuffleListItems<string>(question.answerOptions);

        //Put different questions in the buttons
        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answerOptions[i];
            answerButtons[i].name = answerOptions[i];    
            answerButtons[i].image.color = normalCol; 
        }

        itWasAnswered = false;
    }

    public void ReduceAttempts(int remainingLife)
    {
        answerAttemptsList[remainingLife].color = Color.red;
    }
    
    // This Method instantiate the Easy/Medium/Hard Buttons
    void CreateDifficultyButtons()
    {
        //Here it is checked how many difficulty categories are in the list
        for (int i = 0; i < quizManager.QuizData.Count; i++)
        {
            CategoryBtnScript categoryBtn = Instantiate(difficultyButtonPrefab, scrollHolder.transform);
            categoryBtn.SetButton(quizManager.QuizData[i].categoryName, quizManager.QuizData[i].questions.Count, quizManager.QuizData[i].timeToComplete); // Here we set the button values
            int index = i;
            
            categoryBtn.Btn.onClick.AddListener(() => DifficultyButton(index, quizManager.QuizData[index].categoryName));
        }
    }

    //Method called by Category Button
    private void DifficultyButton(int index, string category)
    {
        quizManager.StartGame(index, category);
        mainMenu.SetActive(false);             
        gamePanel.SetActive(true);            
    }

    // Coroutine that makes sprite blink
    IEnumerator CorrectBlinkImage(Image image)
    {
        for (int i = 0; i < 2; i++)
        {
            image.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            image.color = correctCol;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator WrongBlinkImage(Image image)
    {
        for (int i = 0; i < 2; i++)
        {
            image.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            image.color = correctCol;
            yield return new WaitForSeconds(0.1f);
        }
    }
    void OnClick(Button btn)
    {
        if (quizManager.GameStatus == GameStatus.Playing)
        {
            if (!itWasAnswered)
            {
                itWasAnswered = true;
               
                bool correctAnswer = quizManager.Answer(btn.name);

                if (correctAnswer)
                {
                    StartCoroutine(CorrectBlinkImage(btn.image));
                }
                else
                {
                    StartCoroutine(WrongBlinkImage(btn.image));
                }
            }
        }
    }
    public void RegisterButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
