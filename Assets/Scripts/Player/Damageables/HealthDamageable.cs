using Fusion;
using UnityEngine;
using UnityEngine.Events;

public class HealthDamageable : NetworkBehaviour, IDamageable
{
    [SerializeField] private Indicator indicator;
    [SerializeField] private int maxHealth;
    private UnityEvent<NetworkRunner> onDeath = new();
    [Networked(OnChanged = nameof(UpdateIndicator))] public int Health { get; private set; }

    private void Start()
    {
        Health = maxHealth;
    }
    
    protected static void UpdateIndicator(Changed<HealthDamageable> changed)
    {
        changed.Behaviour.indicator.UpdateIndicator(1f *  changed.Behaviour.Health / changed.Behaviour.maxHealth);
        if (changed.Behaviour.Health <= 0)
        {
            changed.Behaviour.onDeath.Invoke(changed.Behaviour.Runner);
            changed.Behaviour.Runner.Despawn(changed.Behaviour.Object);
        }
    }

    public void Damage(int damagePoints)
    {
        Health -= damagePoints;
    }

    public void AddDeathListener(UnityAction<NetworkRunner> deathListener)
    {
        onDeath.AddListener(deathListener);
    }
}
