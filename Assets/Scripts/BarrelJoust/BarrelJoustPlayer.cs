using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Lobby;
using UnityEngine;
[System.Serializable]
public struct BarrelJoustPlayerSprites
{
    public Sprite Block;
    public Sprite Hit;
    public Sprite Damage;
    public Sprite Ready;
    public Sprite Idle;
}
public class BarrelJoustPlayer : PlayerBehavior
{
    public SpriteRenderer characterRenderer;
    public BarrelJoustPlayerSprites character;
    public Sprite[] sprites;
    public int spriteIndex;
    public int health;
    public int maxHealth;
    public GameObject[] healthCounters;
    public float cooldownLength;
    private float _cooldown;
    public float actionDuration;
    private float _action;
    private int _playerCharacter;
    public const int READY = 0;
    public const int BLOCK = 1;
    public const int HIT = 2;
    public const int DAMAGE = 3;
    public const int IDLE = 4;
    public bool attacked;
    // Start is called before the first frame update
    void Start()
    {
        _cooldown = cooldownLength;
        MinigameManager.Instance.players.Add(this);
        BarrelJoustEnemyManager.Instance.SpawnEnemy();
        if (networkObject.IsOwner)
        {
            networkObject.playerCharacter = LobbyService.Instance.MyMockPlayer.AvatarID;
        }
        _playerCharacter = networkObject.playerCharacter;
        sprites[READY] = MinigameManager.Instance.barrelJoustPlayerSprites[_playerCharacter].Ready;
        sprites[BLOCK] = MinigameManager.Instance.barrelJoustPlayerSprites[_playerCharacter].Block;
        sprites[HIT] = MinigameManager.Instance.barrelJoustPlayerSprites[_playerCharacter].Hit;
        sprites[DAMAGE] = MinigameManager.Instance.barrelJoustPlayerSprites[_playerCharacter].Damage;
        sprites[IDLE] = MinigameManager.Instance.barrelJoustPlayerSprites[_playerCharacter].Idle;
        characterRenderer.sprite = sprites[READY];
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject == null)
            return;
        if (!networkObject.IsOwner)
        {
            spriteIndex = networkObject.spriteIndex;
            health = networkObject.score;
            characterRenderer.sprite = sprites[spriteIndex];
            return;
        }
        if (_action < actionDuration)
        {
            _action += Time.deltaTime;
            return;
        }
        
        if (_cooldown > cooldownLength)
        {
            if (Input.GetKey("z"))
            {
                spriteIndex = BLOCK;
                _action = 0;
                _cooldown = 0;
                AudioManager.PlaySound(AudioManager.Sound.joustBlock);
            }
            else if (Input.GetKey("x"))
            {
                spriteIndex = HIT;
                _action = 0;
                _cooldown = 0;
                BarrelJoustLogic.Instance.TakeDamage();
                AudioManager.PlaySound(AudioManager.Sound.joustHit);
            }
            else
            {
                spriteIndex = READY;
            }
        }
        else
        {
            _cooldown += Time.deltaTime;
        }
        
        networkObject.spriteIndex = spriteIndex;
        networkObject.score = health;
        characterRenderer.sprite = sprites[spriteIndex];
    }

    public void TakeDamage()
    {
        AudioManager.PlaySound(AudioManager.Sound.joustHit);
        healthCounters[maxHealth - health].SetActive(false);
        health--;
        spriteIndex = DAMAGE;
        characterRenderer.sprite = sprites[spriteIndex];
        _cooldown = 0;
    }
}
