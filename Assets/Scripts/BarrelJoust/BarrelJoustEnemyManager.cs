using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelJoustEnemyManager : MonoBehaviour
{
    private static BarrelJoustEnemyManager _instance;
    public List<GameObject> enemies;
    public Sprite[] sprites;
    private int _enemyIndex = 0;
    public static BarrelJoustEnemyManager Instance
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnEnemy()
    {
        enemies[_enemyIndex].SetActive(true);
        _enemyIndex++;
    }
}
