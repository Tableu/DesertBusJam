using System;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class MinigameManager : MinigameManagerBehavior
{
    private static MinigameManager _instance;
    public int playerPrefabIndex;
    public List<Vector3> startPos;
    public List<PlayerBehavior> players;

    public static MinigameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }

    private void Start()
    {
        NetworkManager.Instance.InstantiatePlayer(playerPrefabIndex, startPos[(int)networkObject.MyPlayerId]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
