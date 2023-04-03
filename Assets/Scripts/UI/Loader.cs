using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onLoad;

    void Start()
    {
        onLoad.Invoke();
    }
}
