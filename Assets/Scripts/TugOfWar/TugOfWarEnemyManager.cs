using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TugOfWarEnemyManager : MonoBehaviour
{
    private static TugOfWarEnemyManager _instance;
    public List<GameObject> enemies;
    public Sprite[] sprites;
    public int timerLength;
    private int _enemyIndex = 0;
    private float timer = 0;
    private bool switched = false;
    public static TugOfWarEnemyManager Instance
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > timerLength / 2 && !switched)
        {
            foreach (GameObject enemy in enemies)
            {
                SwitchSprites(enemy,TugOfWarPlayer.STRUGGLE);
            }
            switched = true;
        }

        if (timer > timerLength)
        {
            foreach (GameObject enemy in enemies)
            {
                SwitchSprites(enemy,TugOfWarPlayer.NORMAL);
            }
            switched = false;
            timer = 0;
        }
    }
    private void OnDestroy()
    {
        _instance = null;
    }

    public void SpawnEnemy()
    {
        enemies[_enemyIndex].SetActive(true);
        _enemyIndex++;
    }

    public void SwitchSprites(GameObject enemy, int spriteIndex)
    {
        var spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }
}
