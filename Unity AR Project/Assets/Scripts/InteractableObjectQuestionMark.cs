using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectQuestionMark : InteracbleObject
{
    [Header("UI Panel Being Activated")]
    public UIPanelValues uiPanel;

    // Start is called before the first frame update
    protected override void Start()
    {
        uiPanel = UIManager.Instance.questionPanel;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();

        UIManager.Instance.SlideInOut(uiPanel);

        //TODO: Use scriptable objects to add questions at runtime. SO can hold the questions AND answers
    }
}
