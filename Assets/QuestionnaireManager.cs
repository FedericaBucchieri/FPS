using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using UnityEngine.SceneManagement;

public class QuestionnaireManager : MonoBehaviour
{
    [SerializeField]
    private int questionsNumber;

    [SerializeField]
    private GameObject[] conditionImages;

    [SerializeField]
    private GameObject greetings;

    private string[] answersValues;

    [SerializeField]
    private GameObject activeCanva;

    private void Start()
    {

        // cursor setting
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        answersValues = new string[questionsNumber];

        switch (SceneFlowManager.currentCondition)
        {
            case 'A':
                conditionImages[0].SetActive(true);
                break;
            case 'B':
                conditionImages[1].SetActive(true);
                break;
            case 'C':
                conditionImages[2].SetActive(true);
                break;

        }

    }

    public void setAnswer(string value, int index)
    {
        if (index > questionsNumber)
            return;

        Debug.Log("set answer:" + index + " value:" + value);
        answersValues[index] = value;

    }

    public void sendAnswers()
    {
        SendQuestionnaireAnswerEvent sendEvt = new SendQuestionnaireAnswerEvent();
        sendEvt.answers = answersValues;
        EventManager.Broadcast(sendEvt);

        displayGreetings();
    }

    void displayGreetings()
    {
        char condition = SceneFlowManager.getNextCondition();

        if (condition.Equals('e'))
            SceneManager.LoadScene("EndGameScene");
        else
        {
            activeCanva.SetActive(false);
            greetings.SetActive(true);
        }
    }
}
