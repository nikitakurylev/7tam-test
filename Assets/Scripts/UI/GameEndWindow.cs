using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class GameEndWindow : NetworkBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private Text text;
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_GameEnd(string message)
    {
        text.text = message;
        window.SetActive(true);
    }
}
