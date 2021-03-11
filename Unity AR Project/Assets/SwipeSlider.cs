using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class SwipeSlider : MonoBehaviour
{
    [SerializeField] private Button _swipeRight;
    [SerializeField] private Button _swipeLeft; 
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private int sliderPosition;
    private bool isRunning = false;
    private float timeToSwipe = 0f;
   
    [SerializeField] private CanvasGroup[] canvasGroups;


    void Start()
    {
        Scrollbar _scrollbar = gameObject.GetComponentInParent(typeof(Scrollbar)) as Scrollbar;
        _swipeRight.GetComponentInChildren<Button>().onClick.AddListener(SwipeRight);
        _swipeLeft.GetComponentInChildren<Button>().onClick.AddListener(SwipeLeft);
        sliderPosition = 0;
        timeToSwipe = 0f;
    }

    private void Awake()
    {
        _scrollbar.value = 0;
    }

    private void Update()
    {
        sliderPosition = Mathf.Clamp(sliderPosition, 0, 5);

        
        if (sliderPosition == 0 || isRunning == true)
        {
            _swipeLeft.interactable = false;
        }
        else
        {
            _swipeLeft.interactable = true;
        }
       
        if (sliderPosition == 5 || isRunning == true)
        {
            _swipeRight.interactable = false;
        }
        else
        {
            _swipeRight.interactable = true;
        }
    }

    public void SwipeRight()
    {
        
        StartCoroutine(SliderValue(+ 0.20f));

        sliderPosition++;
        
        if (sliderPosition > 0)
        {
            StartCoroutine(FadeUI.Fade(0, canvasGroups[sliderPosition - 1], 1.5f));
        }
        StartCoroutine(FadeUI.Fade(1, canvasGroups[sliderPosition], 1.5f));
    }

    public void SwipeLeft()
    {
      
        StartCoroutine(SliderValue(- 0.20f));
        
        sliderPosition--;
        
        if (sliderPosition < 5)
        {
            StartCoroutine(FadeUI.Fade(0, canvasGroups[sliderPosition + 1], 1.5f));
        }
        StartCoroutine(FadeUI.Fade(1, canvasGroups[sliderPosition], 1.5f));
    }

   
    IEnumerator SliderValue(float value)
    {
        isRunning = true;
        while (timeToSwipe < 1f)
        {
          
            timeToSwipe+= Time.deltaTime;
            _scrollbar.value = Mathf.Lerp(_scrollbar.value, _scrollbar.value + value, 1f * Time.deltaTime);

            yield return null;
        }

        if (timeToSwipe > 1f)
        {
            timeToSwipe = 0;
        }
        isRunning = false;
    }
}
    
    
   

   



