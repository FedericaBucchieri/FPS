using UnityEngine;
using System.Collections;
using Unity.FPS.Game;
using UnityEngine.SceneManagement;

public class GameTimeManager : MonoBehaviour
{

    bool expired = false;

    // Use this for initialization
    void Start()
    {
        StartTrialEvent evt = new StartTrialEvent();
        EventManager.Broadcast(evt);
    }

    // Update is called once per frame
    void Update()
    {
        if (expired)
            return;

        GameConstants.playedTrialTime += Time.deltaTime;

        Debug.Log(GameConstants.playedTrialTime);

        if (GameConstants.playedTrialTime >= GameConstants.totalTrialTime)
        {
            GameOver();
        }
    }


    void GameOver()
    {
        expired = true;
        EndTrialEvent evt = new EndTrialEvent();
        EventManager.Broadcast(evt);

        // Change condition
        char condition = SceneFlowManager.getNextCondition();
        SceneFlowManager.LevelPlayed = 0;
        GameConstants.playedTrialTime = 0f;

        if (condition.Equals('e'))
            SceneManager.LoadScene("EndGameScene");
        else
            SceneManager.LoadScene("ChangeConditionScene");
    }
}
