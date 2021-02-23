using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigationSign : MonoBehaviour
{
    private int randomSelection;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterBase>(out CharacterBase _characterBase))
        {
            randomSelection = Random.Range(0, 2);
            //Debug.Log(randomSelection);
            _characterBase.chooseAction(randomSelection);
        }
    }
}
