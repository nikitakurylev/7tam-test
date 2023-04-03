using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BarIndicator : Indicator
{
    private SpriteRenderer _spriteRenderer;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(!_spriteRenderer)
            Debug.Log("No SpriteRenderer attached!");
    }

    public override void UpdateIndicator(float value)
    {
        _spriteRenderer.size = new Vector2(value, 1f);
    }
}
