using Fusion;

public class Player : NetworkBehaviour
{
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            transform.Translate(data.Direction.normalized);
        }
    }
}