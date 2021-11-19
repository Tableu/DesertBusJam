using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelJoustLogic : MonoBehaviour
{
    private static BarrelJoustLogic _instance;
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
    public float startTime;

    public static BarrelJoustLogic Instance
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
    // Start is called before the first frame update
    void Start()
    {
        enemy = BarrelJoustEnemyManager.Instance.enemies[0];
        enemyRenderer = enemy.GetComponent<SpriteRenderer>();
        attackTimer = 0;
        startTime = Time.deltaTime;
        if (MapManager.Instance.TugOfWarDifficulty > 1)
        {
            MusicManager.Instance.PlayMusic("minigame2");
        }
        else
        {
            MusicManager.Instance.PlayMusic("minigame1");
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        if (player.spriteIndex != BarrelJoustPlayer.BLOCK)
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

    public void TakeDamage()
    {
        if (spriteIndex != BarrelJoustPlayer.BLOCK)
        {
            healthCounters[maxHealth-health].SetActive(false);
            health--;
            spriteIndex = BarrelJoustPlayer.DAMAGE;
            enemyRenderer.sprite = sprites[spriteIndex];
            attackTimer = 0;
            if (health == 0)
            {
                enemy.SetActive(false);
                AudioManager.PlaySound(AudioManager.Sound.joustHit);
                AudioManager.PlaySound(AudioManager.Sound.crowdCheer01);
                AudioManager.PlaySound(AudioManager.Sound.victory);
                enemyIndex++;
                if (enemyIndex >= MinigameManager.Instance.players.Count)
                {
                    gameObject.SetActive(false);
                    MapManager.Instance.AddPoints(1000-(int)(Time.deltaTime-startTime)*50);
                    MinigameManager.Instance.Win();
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
}
