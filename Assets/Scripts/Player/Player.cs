using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(IMovement))]
public class Player : NetworkBehaviour
{
    private IMovement _movement;
    private void Awake()
    {
        _movement = GetComponent<IMovement>();
        if(_movement == null)
            Debug.LogWarning("No component of IMovement type attached!");
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            _movement.Move(data.Direction);
        }
    }
}