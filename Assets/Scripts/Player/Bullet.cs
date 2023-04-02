using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float speed;
    [Networked] private TickTimer Life { get; set; }

    public void Init()
    {
        GetComponent<Rigidbody2D>()?.AddForce(speed * transform.up, ForceMode2D.Impulse);
        Life = TickTimer.CreateFromSeconds(Runner, 5);
    }
    
    public override void FixedUpdateNetwork()
    {
        if(Life.Expired(Runner))
            Runner.Despawn(Object);
    }
}