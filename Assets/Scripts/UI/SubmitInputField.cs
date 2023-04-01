using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class SubmitInputField : MonoBehaviour
{
    private InputField _inputField;

    void Awake()
    {
        _inputField = GetComponent<InputField>();
        if (!_inputField)
            Debug.LogWarning("No Input Field attached!");
    }

    public void Submit()
    {
        if (!_inputField)
            Debug.LogWarning("Input Field was removed!");
        _inputField.onSubmit.Invoke(_inputField.text);
    }
}