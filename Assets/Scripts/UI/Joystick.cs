using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    [SerializeField] private float limit = 200f;
    private RectTransform _rectTransform;
    private bool _isDragging;
    private Vector2 _position;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (_isDragging)
        {
            transform.position = _position;
            if (_rectTransform.anchoredPosition.sqrMagnitude > limit * limit)
            {
                _rectTransform.anchoredPosition = _rectTransform.anchoredPosition.normalized * limit;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _position = eventData.position;
    }

    public Vector2 GetPosition()
    {
        return _rectTransform.anchoredPosition / limit;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        _position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = Vector2.zero;
        _isDragging = false;
    }
}