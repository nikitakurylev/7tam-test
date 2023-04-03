using Fusion;
using UnityEngine;

public class Coin : NetworkBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Score score = col.GetComponent<Score>();
        if (score)
        {
            score.AddCoin();
            transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-4f, 4f));
        }
    }
}
