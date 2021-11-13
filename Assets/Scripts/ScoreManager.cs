using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    [SerializeField] private int points;
    [SerializeField] private Text moneyText;
    public static ScoreManager Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        _instance = this;
    }
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int score)
    {
        points += score;
    }
}
