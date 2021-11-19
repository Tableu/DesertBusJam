using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MinigameManagerBehavior
{
    private static MinigameManager _instance;
    public int playerPrefabIndex;
    public List<Vector3> startPos;
    public List<PlayerBehavior> players;
    public List<TugOfWarCharacter> tugOfWarCharacters;
    public List<BarrelJoustPlayerSprites> barrelJoustPlayerSprites;
    public Sprite[] hands;

    public static MinigameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            Destroy(this);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        NetworkManager.Instance.InstantiatePlayer(playerPrefabIndex, startPos[(int)networkObject.MyPlayerId]);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    public void Win()
    {
        MapManager.Instance.MinigameWin();
        if (networkObject.IsServer)
        {
            SceneManager.LoadScene("Scenes/MapScene", LoadSceneMode.Single);
        }
    }

    public void Lose()
    {
        MapManager.Instance.MinigameLose();
        if (networkObject.IsServer)
        {
            SceneManager.LoadScene("Scenes/MapScene", LoadSceneMode.Single);
        }
    }
}
