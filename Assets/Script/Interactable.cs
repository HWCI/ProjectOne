using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    
    [Serializable]
    public class OnInteractHandler : UnityEvent
    {
    }
    [SerializeField] public OnInteractHandler onInteract;

    private GameObject target;

    void Start()
    {
        target = this.GetComponent<GameObject>();
    }
    
    
}
