using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class TugOfWarLogic : RopeBehavior
{
    public int timerLength;
    public int scoreGoal;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (networkObject == null)
            return;
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            return;
        }
        timer += Time.fixedDeltaTime;
        if (timer > timerLength)
        {
            timer = 0;
            int totalScore = 0;
            foreach (TugOfWarPlayer player in MinigameManager.Instance.players)
            {
                totalScore += player.networkObject.score;
                player.score = 0;
                player.networkObject.score = 0;
            }
            
            if (totalScore > scoreGoal*MinigameManager.Instance.players.Count)
            {
                var pos = transform.position;
                transform.position = pos + new Vector3(-1, 0, 0);
            }else if (totalScore <= scoreGoal*MinigameManager.Instance.players.Count)
            {
                var pos = transform.position;
                transform.position = pos + new Vector3(1, 0, 0);
            }

            if (transform.position.x > 5)
            {
                MinigameManager.Instance.Lose();
                Time.timeScale = 0;
            }else if (transform.position.x < -5)
            {
                MinigameManager.Instance.Win(400);
                Time.timeScale = 0;
            }
        }
        networkObject.position = transform.position;
    }

    private void SetDifficulty()
    {
        timerLength = 0;
        scoreGoal = 0;
    }
}
