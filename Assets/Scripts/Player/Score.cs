using Fusion;
using UnityEngine;

public class Score : NetworkBehaviour
{
    [SerializeField] private Indicator indicator;
    [Networked(OnChanged = nameof(UpdateIndicator))] private int Coins { get; set; }
    
    protected static void UpdateIndicator(Changed<Score> changed)
    {
        changed.Behaviour.indicator.UpdateIndicator(changed.Behaviour.Coins);
    }

    public void AddCoin()
    {
        Coins++;
    }
}
