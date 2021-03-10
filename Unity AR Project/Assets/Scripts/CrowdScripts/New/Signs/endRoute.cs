using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endRoute : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<characterAI>(out characterAI _characterAI))
        {
            _characterAI.SetInactive();
        }
    }
}
