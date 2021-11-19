using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Lobby;
using BeardedManStudios.Forge.Networking.Unity.Lobby;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public struct TugOfWarCharacter
{
    public Sprite Normal;
    public Sprite Struggle;
}
public class TugOfWarPlayer : PlayerBehavior
{
    public int score;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer handRenderer;
    public Sprite[] sprites;
    public int spriteIndex;
    public int sortingOrder;
    public SortingGroup sortingGroup;
    private string[] _mashKeys = {"z", "x"};
    private int _playerCharacter;
    public const int NORMAL = 0;
    public const int STRUGGLE = 1;

    private void Awake()
    {
        AudioManager.InitializeSoundTimer();
    }
    void Start()
    {
        MinigameManager.Instance.players.Add(this);
        TugOfWarEnemyManager.Instance.SpawnEnemy();
        if (networkObject.IsOwner)
        {
            sortingOrder = -(int) networkObject.MyPlayerId;
            sortingGroup.sortingOrder = sortingOrder;
            networkObject.sortingOrder = sortingOrder;
            networkObject.playerCharacter = LobbyService.Instance.MyMockPlayer.AvatarID;
        }
        _playerCharacter = networkObject.playerCharacter;
        sprites[NORMAL] = MinigameManager.Instance.characters[_playerCharacter].Normal;
        sprites[STRUGGLE] = MinigameManager.Instance.characters[_playerCharacter].Struggle;
        handRenderer.sprite = MinigameManager.Instance.hands[_playerCharacter];
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
            sprites[NORMAL] = MinigameManager.Instance.characters[networkObject.playerCharacter].Normal;
            sprites[STRUGGLE] = MinigameManager.Instance.characters[networkObject.playerCharacter].Struggle;
            handRenderer.sprite = MinigameManager.Instance.hands[networkObject.playerCharacter];
            return;
        }
        if (Input.GetKeyUp(_mashKeys[0]) || Input.GetKeyUp(_mashKeys[1]))
        {
            AudioManager.PlaySound(AudioManager.Sound.tug);
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
