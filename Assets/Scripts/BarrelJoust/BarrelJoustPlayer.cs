using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
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
    public const int READY = 0;
    public const int BLOCK = 1;
    public const int HIT = 2;
    public const int DAMAGE = 3;
    public const int IDLE = 4;
    // Start is called before the first frame update
    void Start()
    {
        _cooldown = cooldownLength;
        MinigameManager.Instance.players.Add(this);
        BarrelJoustEnemyManager.Instance.SpawnEnemy();
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
        if (_cooldown < cooldownLength)
        {
            _cooldown += Time.deltaTime;
            return;
        }
            
        if (Input.GetKey("z"))
        {
            spriteIndex = BLOCK;
            _cooldown = 0;
        }else if (Input.GetKey("x"))
        {
            spriteIndex = HIT;
            _cooldown = 0;
        }
        else
        {
            spriteIndex = READY;
        }
        networkObject.spriteIndex = spriteIndex;
        networkObject.score = health;
        characterRenderer.sprite = sprites[spriteIndex];
    }

    public void TakeDamage()
    {
        healthCounters[maxHealth - health].SetActive(false);
        health--;
        spriteIndex = DAMAGE;
        characterRenderer.sprite = sprites[spriteIndex];
        _cooldown = 0;
    }
}
