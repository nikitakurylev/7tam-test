using Fusion;
using UnityEngine;

public class Score : NetworkBehaviour
{
    [SerializeField] private Indicator indicator;
    [Networked(OnChanged = nameof(UpdateIndicator))] public int Coins { get; private set; }
    
    protected static void UpdateIndicator(Changed<Score> changed)
    {
        changed.Behaviour.indicator.UpdateIndicator(changed.Behaviour.Coins);
    }

    public void AddCoin()
    {
        Coins++;
    }
}
