using Fusion;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    private NetworkRunner _runner;

    private void Awake()
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;
    }

    async void StartGame(GameMode mode, string lobbyName)
    {
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = lobbyName,
            Scene = 1,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public void Host(string lobbyName)
    {
        StartGame(GameMode.Host, lobbyName);
    }

    public void Join(string lobbyName)
    {
        StartGame(GameMode.Client, lobbyName);
    }
}