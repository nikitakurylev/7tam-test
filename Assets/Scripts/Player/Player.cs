using Fusion;
using UnityEngine;

[RequireComponent(typeof(IMovement))]
public class Player : NetworkBehaviour
{
    [SerializeField] private NetworkPrefabRef bulletPrefab;
    [SerializeField] private float fireRate;
    [Networked] private TickTimer delay { get; set; }
    private IMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<IMovement>();
        if (_movement == null)
            Debug.LogWarning("No component of IMovement type attached!");
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            _movement.Move(data.Direction);

            if (delay.ExpiredOrNotRunning(Runner) && data.IsFireDown)
            {
                delay = TickTimer.CreateFromSeconds(Runner, fireRate);
                Runner.Spawn(bulletPrefab, transform.position + transform.up,
                    Quaternion.LookRotation(transform.forward, transform.up), Object.InputAuthority,
                    (runner, o) => { o.GetComponent<Bullet>().Init(); });
            }
        }
    }
}