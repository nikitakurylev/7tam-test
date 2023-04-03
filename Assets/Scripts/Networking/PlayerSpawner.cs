using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef playerPrefab;
    private IInputMethod _inputMethod;
    private readonly Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new();
    private int alivePlayerCount;
    private UnityEvent<string> _onGameEnd = new();

    private void OnPlayerDied(NetworkRunner runner)
    {
        if (!runner.IsServer) return;
        alivePlayerCount--;
        if (alivePlayerCount != 1)
            return;
        Player winner = _spawnedCharacters.Values.Select(o => o.GetComponent<Player>()).Aggregate((p1, p2) => p1.Score > p2.Score ? p1 : p2);
        FindObjectOfType<GameEndWindow>()?.RPC_GameEnd("Player " + (winner.PlayerNumber + 1) + " wins with " + winner.Score + " coins!");
    }

    public void AddGameEndListener(UnityAction<string> onGameEnd)
    {
        _onGameEnd.AddListener(onGameEnd);
    }
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        _inputMethod = new AxisInputMethod();
        if (!runner.IsServer) return;
        Vector3 spawnPosition =
            new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 2 - 6, 0, 0);
        NetworkObject networkPlayerObject =
            runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
        networkPlayerObject.GetComponent<Player>().PlayerNumber = _spawnedCharacters.Count;
        networkPlayerObject.GetComponent<HealthDamageable>()?.AddDeathListener(OnPlayerDied);
        _spawnedCharacters.Add(player, networkPlayerObject);
        alivePlayerCount++;
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
            OnPlayerDied(runner);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = _inputMethod?.GetInputData() ?? new NetworkInputData();
        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
        byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}