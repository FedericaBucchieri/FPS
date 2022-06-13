using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.UI;

public class InitManager : MonoBehaviour
{
    public GameObject participantID;
    public GameObject participantError;

    public Button startButton;

    [SerializeField]
    private string sceneToLoad;

    char[] array;


    public void SetParticipantID(string participantID)
    {
        if (participantID == null || participantID == "")
        {
            participantError.SetActive(true);
            return;

        }

        int id = int.Parse(participantID);

        if (id <= 0 | id > 100)
            participantError.SetActive(true);
        else
            GameConstants.participantID = id;

        switch (id % 6)
        {
            case 1:
                array = GameConstants.condition_1.ToCharArray();
                break;
            case 2:
                array = GameConstants.condition_2.ToCharArray();
                break;
            case 3:
                array = GameConstants.condition_3.ToCharArray();
                break;
            case 4:
                array = GameConstants.condition_4.ToCharArray();
                break;
            case 5:
                array = GameConstants.condition_5.ToCharArray();
                break;
            case 0:
                array = GameConstants.condition_6.ToCharArray();
                break;

        }

        foreach (char c in array)
        {
            SceneFlowManager.testCondition = new List<char>();
            SceneFlowManager.testCondition.Add(c);
        }
    }



    public void deactivateParticipantError()
    {
        participantError.SetActive(false);
    }

    private void Update()
    {

        if (GameConstants.participantID != 0)
            startButton.interactable = true;
    }

    public void StartTrial()
    {
        CreateFile();

        SceneManager.LoadScene(sceneToLoad);
    }


    public void CreateFile()
    {
        // path to user Desktop
        string path = Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/Log_" + GameConstants.participantID +".txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Participant ID, Date, Test Condition, Current Condition, Timestamp, Event Type \n");
        }

        GameConstants.logFilePath = path;

    }
}
