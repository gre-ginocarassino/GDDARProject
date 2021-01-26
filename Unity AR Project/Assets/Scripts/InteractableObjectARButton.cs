using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectARButton : InteracbleObject
{
    public BoardController boardCont;
    public PlaceController thePlace;

    public int loadPlaceType;

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Interact()
    {
        boardCont.NewSection(thePlace);
    }
}
