using UnityEngine;

public class AxisInputMethod : IInputMethod
{
    public Vector2 GetDirection()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
