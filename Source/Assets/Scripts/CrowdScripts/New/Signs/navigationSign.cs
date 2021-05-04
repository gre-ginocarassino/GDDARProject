using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigationSign : MonoBehaviour
{
    private int randomSelection;
    private MainController Main_Controller;
    private BoardController Board_Controller;

    //threshold means the num of pop in the city
    public int threshold;
    public int maxThreshold;

    public float chance;

    private void Start()
    {
        Main_Controller = (MainController)FindObjectOfType(typeof(MainController));
        Board_Controller = (BoardController)FindObjectOfType(typeof(BoardController));
    }

    private void Update()
    {
        if (threshold > (maxThreshold / 2))
        {
            float a = maxThreshold - threshold;
            chance = (a / maxThreshold) * 100;
            //Debug.Log(a);
        }
        else
        {
            //by default 70 percent chance to become a tourist between 0 and 99 if the pop(threshold) is less 50% of max threshold
            chance = 69;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(chance);

        if (other.TryGetComponent<characterAI>(out characterAI _characterAI))
        {
            //between 0 and 99
            randomSelection = Random.Range(0, 100);

            if (threshold < maxThreshold)
            {
                if (randomSelection < chance) //Become tourist
                {
                  _characterAI.selectAction(0); //0 = become tourist
                  threshold += 1;
                }
                else //Become passenger
                {
                  _characterAI.selectAction(1); //1 = passenger
                }
            }
        }
    }

    //reset threshold
    private void OnDisable()
    {
        threshold = 0;
    }
}
