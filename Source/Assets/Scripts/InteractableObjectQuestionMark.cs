using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectQuestionMark : InteracbleObject
{
    [Header("Variables")]
    public bool completed;
    public GameObject completeTick;
    public GameObject questionMark;

    // Start is called before the first frame update
    protected override void Start()
    {
        //uiPanel = UIManager.Instance.questionPanel;

        if (completed)
            Completed();
   
        StartCoroutine(Pulse());
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public override void Interact()
    {
        base.Interact();

        UIManager.Instance.SlideInOut(UIManager.Instance.questionPanel);

        //TODO: Use scriptable objects to add questions at runtime. SO can hold the questions AND answers
        
        //Just for testing. Auto complete on click
        Completed();
    }

    public void Completed()
    {
        completeTick.SetActive(true);
        questionMark.SetActive(false);
    }

    IEnumerator Pulse()
    {
        transform.DOScaleY(1.1f, 3);

        yield return new WaitForSeconds(3);

        transform.DOScaleY(0.9f, 3);

        yield return new WaitForSeconds(3);

        StartCoroutine(Pulse());
    }
}
