using UnityEngine;

public class TextIndicator : Indicator
{
    private TextMesh _textMesh;
    
    void Start()
    {
        _textMesh = GetComponent<TextMesh>();
        if(!_textMesh)
            Debug.Log("No TextMesh attached!");
    }

    private void FixedUpdate()
    {
        transform.up = Vector3.up;
    }
    
    public override void UpdateIndicator(float value)
    {
        _textMesh.text = value.ToString();
    }
}
