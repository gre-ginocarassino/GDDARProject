using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endRoute : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterBase>(out CharacterBase _characterBase))
        {
            _characterBase.setInactive();
        }
    }
}
