using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelJoustLogic : MonoBehaviour
{
    public BarrelJoustPlayer player;
    public GameObject enemy;
    public int playerIndex;
    public int enemyIndex;
    public SpriteRenderer enemyRenderer;
    public int spriteIndex;
    public Sprite[] sprites;
    public int maxHealth;
    public int health;
    public GameObject[] healthCounters;
    public float attackTimer;
    public float attackTimerLength;
    // Start is called before the first frame update
    void Start()
    {
        enemy = BarrelJoustEnemyManager.Instance.enemies[0];
        enemyRenderer = enemy.GetComponent<SpriteRenderer>();
        attackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            
        }else if (Input.GetKeyDown("x"))
        {
            if (spriteIndex != BarrelJoustPlayer.BLOCK)
            {
                healthCounters[maxHealth-health].SetActive(false);
                health--;
                if (health == 0)
                {
                    enemy.SetActive(false);
                    enemyIndex++;
                    if (enemyIndex >= MinigameManager.Instance.players.Count)
                    {
                        gameObject.SetActive(false);
                        return;
                    }

                    enemy = BarrelJoustEnemyManager.Instance.enemies[enemyIndex];
                    enemy.transform.position = transform.position;
                    enemyRenderer = enemy.GetComponent<SpriteRenderer>();
                    health = maxHealth;
                    foreach(GameObject healthCounter in healthCounters)
                        healthCounter.SetActive(true);
                }
            }
        }
        
        if(player == null)
            player = MinigameManager.Instance.players[playerIndex].gameObject.GetComponent<BarrelJoustPlayer>();
        attackTimer += Time.deltaTime;
        if (attackTimer < attackTimerLength)
        {
            if (attackTimer > attackTimerLength / 4)
            {
                spriteIndex = BarrelJoustPlayer.READY;
                enemyRenderer.sprite = sprites[spriteIndex];
            }
            return;
        }

        spriteIndex = BarrelJoustPlayer.HIT;
        enemyRenderer.sprite = sprites[spriteIndex];
        if (player.spriteIndex == BarrelJoustPlayer.HIT || player.spriteIndex == BarrelJoustPlayer.READY)
        {
            player.TakeDamage();
            if (player.health == 0)
            {
                player.gameObject.SetActive(false);
                playerIndex++;
                if (playerIndex >= MinigameManager.Instance.players.Count)
                {
                    gameObject.SetActive(false);
                    return;
                }

                player = MinigameManager.Instance.players[playerIndex].gameObject.GetComponent<BarrelJoustPlayer>();
                player.transform.position = transform.position;
            }
        }

        attackTimer = 0;
    }
}
