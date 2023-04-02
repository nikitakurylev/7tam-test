using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float speed;
    private Rigidbody2D _rigidbody;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (!_rigidbody)
            Debug.LogWarning("No Rigidbody2D attached!");
    }

    public void Move(Vector2 direction)
    {
        Vector2 velocity = direction.normalized * speed;
        _rigidbody.velocity = velocity;
        if (velocity.magnitude > 0)
            transform.up = velocity;
    }
}
