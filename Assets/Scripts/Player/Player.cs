using Fusion;
using UnityEngine;

[RequireComponent(typeof(IMovement))]
public class Player : NetworkBehaviour
{
    [SerializeField] private NetworkPrefabRef bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private Color[] colors;
    [SerializeField] private SpriteRenderer body;
    [Networked] private TickTimer Delay { get; set; }
    [Networked(OnChanged = nameof(UpdateColor))] public int PlayerNumber { get; set; }
    private IMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<IMovement>();
        if (_movement == null)
            Debug.LogWarning("No component of IMovement type attached!");
    }
        
    protected static void UpdateColor(Changed<Player> changed)
    {
        changed.Behaviour.body.color = changed.Behaviour.colors[changed.Behaviour.PlayerNumber % changed.Behaviour.colors.Length];
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            _movement.Move(data.Direction);

            if (Delay.ExpiredOrNotRunning(Runner) && data.IsFireDown)
            {
                Delay = TickTimer.CreateFromSeconds(Runner, fireRate);
                Runner.Spawn(bulletPrefab, transform.position + transform.up,
                    Quaternion.LookRotation(transform.forward, transform.up), Object.InputAuthority,
                    (runner, o) => { o.GetComponent<Bullet>().Init(); });
            }
        }
    }
}