using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTypes : MonoBehaviour
{
    enum TypesOfEvents
    {
        EiffelTower,
        LondonEye,
        TheShard,
        BigBen,
        NavigationSign
    }
    private int randomSelection;
    public int threshold;
    public int maxThreshold;

    public event EventHandler Navigation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<characterAI>(out characterAI _characterAI))
        {
            Navigation?.Invoke(this, EventArgs.Empty);
        }
    }

}
