using Fusion;
using UnityEngine;

public class HealthDamageable : NetworkBehaviour, IDamageable
{
    [SerializeField] private Indicator indicator;
    [SerializeField] private int maxHealth;
    [Networked(OnChanged = nameof(UpdateIndicator))] private int Health { get; set; }

    private void Start()
    {
        Health = maxHealth;
    }
    
    protected static void UpdateIndicator(Changed<HealthDamageable> changed)
    {
        changed.Behaviour.indicator.UpdateIndicator(1f *  changed.Behaviour.Health / changed.Behaviour.maxHealth);
    }

    public void Damage(int damagePoints)
    {
        Health -= damagePoints;
        Debug.Log(Health);
    }
}
