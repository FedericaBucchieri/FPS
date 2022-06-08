using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InitManager : MonoBehaviour
{
    public GameObject participantID;
    public GameObject testCondition;
    public GameObject numberOfTrials;

    public GameObject participantError;
    public GameObject conditionError;
    public GameObject numberError;

    public Button startButton;

    [SerializeField]
    private string sceneToLoad;

    public void SetParticipantID(string participantID)
    {
        int id = int.Parse(participantID);

        if (id <= 0 | id > 100)
            participantError.SetActive(true);
        else
            GameConstants.participantID = id;
        Debug.Log(GameConstants.participantID);
    }

    public void SetTestCondition(string testCondition)
    {
        char[] conditions = testCondition.ToCharArray();
        Debug.Log(conditions.Length);

        foreach (char c in conditions)
        {
            if (c.Equals('A') || c.Equals('B') || c.Equals('C'))
                SceneFlowManager.testCondition.Add(c);
            else
            {
                conditionError.SetActive(true);
                SceneFlowManager.testCondition = new List<char>();
                return;
            }
        }

        Debug.Log(SceneFlowManager.testCondition.ToString());
    }

    public void SetNumberOfTrials(string numberOfTrials)
    {
        int num = int.Parse(numberOfTrials);

        if (num <= 0 | num > 10)
            numberError.SetActive(true);
        else 
            SceneFlowManager.numberOfTrials = int.Parse(numberOfTrials);
        Debug.Log(SceneFlowManager.numberOfTrials);
    }

    public void deactivateParticipantError()
    {
        participantError.SetActive(false);
    }

    public void deactivateNumberError()
    {
        numberError.SetActive(false);
    }

    public void deactivateConditionError()
    {
        conditionError.SetActive(false);
    }

    private void Update()
    {
        if (GameConstants.participantID != 0 &&
            SceneFlowManager.numberOfTrials != 0 &&
            SceneFlowManager.testCondition.Count != 0)
            startButton.interactable = true;
    }

    public void StartTrial()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
