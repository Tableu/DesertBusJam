using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TugOfWarPlayer : PlayerBehavior
{
    public int score;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public int spriteIndex;
    public int sortingOrder;
    public SortingGroup sortingGroup;
    private string[] _mashKeys = {"z", "x"};
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
        if (Input.GetKeyUp(_mashKeys[0]) || Input.GetKeyUp(_mashKeys[1]))
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
    public string[] RandomizeKeys()
    {
        string keys = "abcdefghijklmnopqrstuvwxyz";
        int keyIndex = 0;
        for (int i = 0; i < 2; i++)
        {
            keyIndex = Random.Range(0, keys.Length);
            _mashKeys[i] = keys[keyIndex].ToString();
            keys = keys.Remove(keyIndex, 1);
        }
        return _mashKeys;
    }
}
