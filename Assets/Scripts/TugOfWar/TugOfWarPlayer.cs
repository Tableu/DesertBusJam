using System;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using UnityEngine.Rendering;

public class TugOfWarPlayer : PlayerBehavior
{
    public int score;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public int spriteIndex;
    public int sortingOrder;
    public SortingGroup sortingGroup;
    public const int NORMAL = 0;
    public const int STRUGGLE = 1;
    void Start()
    {
        MinigameManager.Instance.players.Add(this);
        TugOfWarEnemyManager.Instance.SpawnEnemy();
        if (networkObject.IsOwner)
        {
            sortingOrder = -(int) networkObject.MyPlayerId;
            sortingGroup.sortingOrder = sortingOrder;
            networkObject.sortingOrder = sortingOrder;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject == null)
            return;
        if (!networkObject.IsOwner)
        {
            score = networkObject.score;
            spriteRenderer.sprite = sprites[networkObject.spriteIndex];
            sortingGroup.sortingOrder = networkObject.sortingOrder;
            return;
        }
        if (Input.GetKeyUp("z") || Input.GetKeyUp("x"))
        {
            score++;
        }

        if (score > 5)
        {
            spriteRenderer.sprite = sprites[STRUGGLE];
            spriteIndex = STRUGGLE;
        }
        else
        {
            spriteRenderer.sprite = sprites[NORMAL];
            spriteIndex = NORMAL;
        }
        networkObject.score = score;
        networkObject.spriteIndex = spriteIndex;
    }
}
