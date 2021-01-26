using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaceController : MonoBehaviour
{
    public BoardController boardController;

    [Header("Base Variables")]
    public GameObject baseSectionsPrefabs;
    public GameObject baseSections;

    public virtual void Awake()
    {
        //setting the variables we need as this object is going to be spawned at runtime :)
        boardController = (BoardController)FindObjectOfType(typeof(BoardController));
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void LoadPlace()
    {
        boardController.activeSection = this;

        if (baseSections == null)
        {
            baseSections = Instantiate(baseSectionsPrefabs, transform);
        }

        baseSections.transform.DOScaleY(1, 1);
    }

    public virtual void ShrinkPlace()
    {
        baseSections.transform.DOScaleY(0, 1);
    }
}
