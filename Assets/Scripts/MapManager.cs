using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MapBehavior
{
    private static MapManager _instance;
    [SerializeField] private int points;
    [SerializeField] private Text moneyText;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject canvas;
    private int _difficulty = 0;
    public int Points => points;
    public int Difficulty => _difficulty;
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
        SceneManager.sceneLoaded += DisableCanvas;
        points = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (networkObject == null)
            return;
        if (!networkObject.IsOwner)
        {
            points = networkObject.score;
            slider.value = points;
            moneyText.text = points.ToString();
            return;
        }
        networkObject.score = points;
    }

    public void AddPoints(int score)
    {
        points += score;
        slider.value = points;
        moneyText.text = points.ToString();
        if (points > 200)
        {
            _difficulty = 1;
        }
    }

    public void LoadMinigame()
    {
        if (networkObject.IsServer)
        {
            SceneManager.LoadScene("Scenes/BarrelJoust");
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
