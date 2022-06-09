using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;
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
        if(participantID == null || participantID == "")
        {
            participantError.SetActive(true);
            return;

        }

        int id = int.Parse(participantID);

        if (id <= 0 | id > 100)
            participantError.SetActive(true);
        else
            GameConstants.participantID = id;
    }

    public void SetTestCondition(string testCondition)
    {

        if (testCondition == null || testCondition == "")
        {
            conditionError.SetActive(true);
            return;

        }


        char[] conditions = testCondition.ToCharArray();

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

    }

    public void SetNumberOfTrials(string numberOfTrials)
    {
        if (numberOfTrials == null || numberOfTrials == "")
        {
            numberError.SetActive(true);
            return;

        }

        int num = int.Parse(numberOfTrials);

        if (num <= 0 | num > 10)
            numberError.SetActive(true);
        else 
            SceneFlowManager.numberOfTrials = int.Parse(numberOfTrials);

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
