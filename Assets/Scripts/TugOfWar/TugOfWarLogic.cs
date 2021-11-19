using BeardedManStudios.Forge.Networking.Generated;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TugOfWarDifficulty
{
    public float timerLength;
    public float keyTimerLength;
    public int scoreGoal;
}
public class TugOfWarLogic : RopeBehavior
{
    public float timerLength;
    public float keyTimerLength;
    public int scoreGoal;
    public GameObject rope;
    public TugOfWarDifficulty[] difficultyLevels;
    private float _timer;
    private float _keyTimer;
    private float startTime;
    public Text[] mashKeysText;
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
        _keyTimer = 0;
        startTime = Time.deltaTime;
        SetDifficulty();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (networkObject == null)
            return;

        if (!networkObject.IsOwner)
        {
            rope.transform.position = networkObject.position;
            return;
        }
        _timer += Time.fixedDeltaTime;
        _keyTimer += Time.fixedDeltaTime;
        if (_timer > timerLength)
        {
            _timer = 0;
            int totalScore = 0;
            
            foreach (TugOfWarPlayer player in MinigameManager.Instance.players)
            {
                totalScore += player.networkObject.score;
                player.score = 0;
                player.networkObject.score = 0;
                if (player.networkObject.IsOwner && _keyTimer > keyTimerLength)
                {
                    string[] mashKeys = player.RandomizeKeys();
                    mashKeysText[0].text = mashKeys[0].ToUpper();
                    mashKeysText[1].text = mashKeys[1].ToUpper();

                    _keyTimer = 0;
                }
            }
            
            if (totalScore > scoreGoal*MinigameManager.Instance.players.Count)
            {
                var pos = rope.transform.position;
                rope.transform.position = pos + new Vector3(-1, 0, 0);
                MapManager.Instance.AddPoints(totalScore);
            }else if (totalScore <= scoreGoal*MinigameManager.Instance.players.Count)
            {
                var pos = rope.transform.position;
                rope.transform.position = pos + new Vector3(1, 0, 0);
            }

            if (rope.transform.position.x > 5)
            {
                MinigameManager.Instance.Lose();
                Time.timeScale = 0;
            }else if (rope.transform.position.x < -5)
            {
                MinigameManager.Instance.Win();
                MapManager.Instance.AddPoints(1000-(int)(Time.deltaTime-startTime)*50);
                Time.timeScale = 0;
            }
        }
        networkObject.position = rope.transform.position;
    }

    private void SetDifficulty()
    {
        if (MapManager.Instance.TugOfWarDifficulty < difficultyLevels.Length)
        {
            TugOfWarDifficulty settings = difficultyLevels[MapManager.Instance.TugOfWarDifficulty];
            timerLength = settings.timerLength;
            keyTimerLength = settings.keyTimerLength;
            scoreGoal = settings.scoreGoal;
        }
    }
}
