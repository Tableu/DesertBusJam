using UnityEngine;

public class PlayCredits : MonoBehaviour
{
    public float timerLength;
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > timerLength)
        {
            MusicManager.Instance.PlayMusic("credits");
            Destroy(gameObject);
        }
    }
}
