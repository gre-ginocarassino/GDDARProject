using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigationSign : MonoBehaviour
{
    private int randomSelection;

    public int threshold;
    public int maxThreshold;
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
