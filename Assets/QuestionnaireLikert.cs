using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class QuestionnaireLikert : MonoBehaviour
{
    public QuestionnaireToggleGroup[] toggleGroups;
    int likertSize = 7;


    public void hideLikert()
    {
        this.gameObject.SetActive(false);
    }

    public void sendAnswers()
    {
        int[] answers = new int[likertSize];

        for (int i = 0; i < toggleGroups.Length; i++)
        {
            QuestionnaireAnswerEvent evt = new QuestionnaireAnswerEvent();
            evt.answer = toggleGroups[i].getAnswer();
            evt.questionIndex = toggleGroups[i].questionIndex.ToString();
            EventManager.Broadcast(evt);
        }
    }

}
