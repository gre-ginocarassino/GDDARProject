using System;
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
    [SerializeField] private TextMeshProUGUI scoreText, timerText, gradingText;
    [SerializeField] private List<Image> answerAttemptsList;
    
    [SerializeField] public List<Image> scoreImageList;
    [SerializeField] private GameObject gameOverPanel, mainMenu, gamePanel;
    [SerializeField] private Color correctCol, wrongCol, normalCol; 
    [SerializeField] private TextMeshProUGUI questionInfoText; 
    [SerializeField] private List<Button> answerButtons;
    public CanvasGroup SpotWelcomePage, SpotQuizUI, GameOverUI;
    
    [SerializeField] public Image timerImage;
    [SerializeField] private Button _interactBtn;
    [SerializeField] private Button _closeBtn;

    [SerializeField] private GameObject[] _buttonsPos;
   
    public Button _retryButton;
    [SerializeField] public TextMeshProUGUI endGameReasonText;

     
    private Question _question;          //store current question data
    private bool itWasAnswered = false;      //bool to keep track if answered or not
  

    public TextMeshProUGUI TimerText { get => timerText; }

    public TextMeshProUGUI GradingText { get => gradingText; }
    public TextMeshProUGUI ScoreText { get => scoreText; }
    public GameObject GameOverPanel { get => gameOverPanel; }

  

    private void Start()
    {
        
        CanvasGroup _canvasGroup = GetComponent<CanvasGroup>();
        TextMeshProUGUI questionInfoText = GetComponent<TextMeshProUGUI>();
        
        
        Button btn = _interactBtn.GetComponent<Button>();
        btn.onClick.AddListener(CloseLandMark);
        if (_closeBtn)
        {
            Button btn1 = _closeBtn.GetComponent<Button>();
            btn1.onClick.AddListener(OpenLandMark);
        }

       
        foreach (var button in answerButtons)
        {
            var button1 = button;
            button.onClick.AddListener(() => OnClick(button1));
        }
        CreateDifficultyButtons();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        CheckAlpha();
    }

    private void CheckAlpha()
    {
        if (GameOverUI.alpha < 0.1)
        {
            gameOverPanel.gameObject.SetActive(false);
           
        }
        if (GameOverUI.alpha > 0.1)
        {
            gameOverPanel.gameObject.SetActive(true);
            
        }
        if (SpotQuizUI.alpha < 0.1)
        {
            gamePanel.gameObject.SetActive(false);
        }
        if (SpotQuizUI.alpha > 0.1)
        {
            gamePanel.gameObject.SetActive(true);
        }
        if (SpotWelcomePage.alpha < 0.1)
        {
            mainMenu.gameObject.SetActive(false);
        }
        if (SpotWelcomePage.alpha > 0.1)
        {
            mainMenu.gameObject.SetActive(true);
        }
    }

    private void CloseLandMark()
    {
        StartCoroutine(FadeUI.Fade(0, SpotWelcomePage,1));
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void OpenLandMark()
    {
        
        StartCoroutine(FadeUI.Fade(1, SpotWelcomePage,1));
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
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[i];
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
           // CategoryBtnScript categoryBtn = Instantiate(difficultyButtonPrefab, scrollHolder.transform);
           CategoryBtnScript categoryBtn = Instantiate(difficultyButtonPrefab, _buttonsPos[i].transform);

           categoryBtn.SetButton(quizManager.QuizData[i].categoryName, quizManager.QuizData[i].questions.Count, quizManager.QuizData[i].timeToComplete); // Here we set the button values
            int index = i;
            
            categoryBtn.Btn.onClick.AddListener(() => DifficultyButton(index, quizManager.QuizData[index].categoryName));
        }
    }

    //Method called by Category Button
    private void DifficultyButton(int index, string category)
    {
        StartCoroutine(FadeUI.Fade(1, SpotQuizUI, 0.8f));
        quizManager.StartGame(index, category);
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
            image.color = wrongCol;
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

    public void RetryButton()
    {
        _retryButton.interactable = false;
        StartCoroutine(FadeUI.Fade(0, SpotQuizUI, 1f));
      //  StartCoroutine(FadeUI.Fade( 1,SpotWelcomePage, 1f));
        StartCoroutine(FadeUI.Fade( 0,GameOverUI, 1f));
        for (int i = 0; i < answerAttemptsList.Count; i++)
        {
            answerAttemptsList[i].color = normalCol;
        }
    }
    
   
    
   
}
