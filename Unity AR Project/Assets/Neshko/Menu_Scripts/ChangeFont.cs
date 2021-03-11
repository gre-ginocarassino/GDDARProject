using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeFont : MonoBehaviour
{
    [SerializeField]
    private ProjectFonts projectFonts;
    [SerializeField]
    private List<TextMeshProUGUI> allTextFields;
    [SerializeField]
    private List<float> startMinSize;
    [SerializeField]
    private GameObject[] enableObjectsHoldingText;
    
    private int fontStyle = 0;
    private int sizeSteps = 0;
    
    void Awake()
    {
        if(projectFonts == null)
            projectFonts = GetComponent<ProjectFonts>();
    }
    private void Start()
    {
        allTextFields = new List<TextMeshProUGUI>(FindObjectsOfType<TextMeshProUGUI>());
        
        foreach (var textInProject in allTextFields)
        {
            startMinSize.Add(textInProject.fontSizeMin);
        }
    }
    // In this method we change the typeface family
    public void ChangeFontFamily()
    {
        allTextFields = new List<TextMeshProUGUI>(FindObjectsOfType<TextMeshProUGUI>());
        fontStyle++;
        
        if (fontStyle == projectFonts.fontsList.Count)
        {
            fontStyle = 0;
        }
        foreach (var textInProject in allTextFields)
        {
            textInProject.font = projectFonts.fontsList[fontStyle];
        }
    }
    // With this method we change the font size
    public void ChangeFontsize()
        {
            if (sizeSteps > 10)
            {
                foreach (var VARIABLE in allTextFields)
                {
                    VARIABLE.fontSize -= 20;
                }
                sizeSteps = 0;
            }
        
            sizeSteps++;
            allTextFields = new List<TextMeshProUGUI>(FindObjectsOfType<TextMeshProUGUI>());
            foreach (var textInProject in allTextFields)
            {
                textInProject.fontSize += 2;
            }
        }
    public void EnableObjectsWithText()
    {
        for (int i = 0; i < enableObjectsHoldingText.Length; i++)
        {
            enableObjectsHoldingText[i].gameObject.active = !enableObjectsHoldingText[i].gameObject.activeSelf;
        }
    }
    
    // In this class we store all project fonts
    [System.Serializable]
    public class ProjectFonts
    {
        [SerializeField] public List<TMP_FontAsset> fontsList;
    }

    
}
