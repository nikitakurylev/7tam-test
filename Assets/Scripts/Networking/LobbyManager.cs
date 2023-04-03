using System.Linq;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private NetworkRunner _runner;

    private void Awake()
    {
        var otherLobbyManager = FindObjectsOfType<LobbyManager>().FirstOrDefault(otherLobbyManager => otherLobbyManager != this);
        DestroyImmediate(otherLobbyManager?.gameObject);
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;
    }

    async void StartGame(GameMode mode, string lobbyName)
    {
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = lobbyName,
            Scene = SceneManager.GetSceneByName("Game").buildIndex,
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