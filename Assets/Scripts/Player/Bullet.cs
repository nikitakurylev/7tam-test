using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damagePoints; 
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<IDamageable>()?.Damage(damagePoints);
        Runner.Despawn(Object);
    }
}