using UnityEngine;

public class AxisInputMethod : IInputMethod
{
    public NetworkInputData GetInputData()
    {
        var data = new NetworkInputData();
        data.Direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        data.IsFireDown = Input.GetButton("Fire1");
        return data;
    }
}
