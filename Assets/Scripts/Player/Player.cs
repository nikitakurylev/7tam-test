using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : NetworkBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rigidbody2D;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if(!_rigidbody2D)
            Debug.LogWarning("No Rigidbody2D attached!");
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            _rigidbody2D.velocity = data.Direction.normalized * speed;
        }
    }
}