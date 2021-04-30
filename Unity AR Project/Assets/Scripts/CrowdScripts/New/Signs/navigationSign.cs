using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigationSign : MonoBehaviour
{
    private int randomSelection;
    private MainController Main_Controller;
    private BoardController Board_Controller;

    public int threshold;
    public int maxThreshold;

    private void Start()
    {
        Main_Controller = (MainController)FindObjectOfType(typeof(MainController));
        Board_Controller = (BoardController)FindObjectOfType(typeof(BoardController));
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<characterAI>(out characterAI _characterAI))
        {
                randomSelection = Random.Range(0, 2);

                if (threshold < maxThreshold)
                {
                    if (randomSelection == 0) //Become tourist
                    {
                        _characterAI.selectAction(randomSelection);
                        threshold += 1;
                    }
                    else //Become passenger
                    {
                        _characterAI.selectAction(randomSelection);
                    }
                }
        }
    }
}
