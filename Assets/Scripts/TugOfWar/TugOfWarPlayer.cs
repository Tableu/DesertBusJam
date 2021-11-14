using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class TugOfWarPlayer : PlayerBehavior
{
    public int score;
    void Start()
    {
        MinigameManager.Instance.players.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject == null)
            return;
        if (!networkObject.IsOwner)
        {
            score = networkObject.score;
            return;
        }
        if (Input.GetKeyUp("z") || Input.GetKeyUp("x"))
        {
            score++;
        }
        networkObject.score = score;
    }
}
