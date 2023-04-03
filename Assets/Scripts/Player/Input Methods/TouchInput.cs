using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour, IInputMethod
{
    [SerializeField] private MyButton button;
    [SerializeField] private Joystick joystick;
    
    public NetworkInputData GetInputData()
    {
        var data = new NetworkInputData();
        data.IsFireDown = button.IsDown;
        data.Direction = joystick.GetPosition();
        return data;
    }
}
