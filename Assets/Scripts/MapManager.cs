using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MapBehavior
{
    private static MapManager _instance;
    [SerializeField] private int points;
    [SerializeField] private Text moneyText;
    [SerializeField] private GameObject van;
    [SerializeField] private Vector3[] vanPositions;
    [SerializeField] private GameObject canvas;
    private int _tugOfWarDifficulty = 0;
    private int _barrelJoustDifficulty = 0;
    private int _posIndex = 0;
    public int Points => points;
    public int TugOfWarDifficulty => _tugOfWarDifficulty;
    public int BarrelJoustDifficulty => _barrelJoustDifficulty;
    public static MapManager Instance
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
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(van);
        SceneManager.sceneLoaded += DisableCanvas;
        points = 0;
        MusicManager.Instance.PlayMusic("gas_station");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (networkObject == null)
            return;
        if (!networkObject.IsOwner)
        {
            points = networkObject.score;
            van.transform.position = networkObject.position;
            moneyText.text = points.ToString();
            return;
        }
        networkObject.score = points;
        networkObject.position = van.transform.position;
    }

    public void MinigameEnd()
    {
        van.SetActive(true);
        if (_posIndex < vanPositions.Length)
        {
            van.transform.position = vanPositions[_posIndex];
            if (_posIndex % 2 == 1)
            {
                _barrelJoustDifficulty++;
            }
            else
            {
                _tugOfWarDifficulty++;
            }
            MusicManager.Instance.PlayMusic("gas_station");
            _posIndex++;
        }
    }

    public void AddPoints(int score)
    {
        if (score > 0)
        {
            points += score;
            moneyText.text = points.ToString();
        }
    }

    public void LoadMinigame()
    {
        van.SetActive(false);
        if (networkObject.IsServer)
        {
            if (_posIndex % 2 == 1)
            {
                SceneManager.LoadScene("Scenes/BarrelJoust");
            }
            else
            {
                SceneManager.LoadScene("Scenes/TugOfWar");
            }
        }
    }

    public void DisableCanvas(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("MapScene"))
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }
}
