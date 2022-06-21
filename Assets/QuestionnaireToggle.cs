using UnityEngine;
using Unity.FPS.Game;
using System.Collections;

public class QuestionnaireToggle : MonoBehaviour
{
    public int index = 0;
    public QuestionnaireManager manager;

    public void sendAnswer(string value)
    {
        manager.setAnswer(value, index);
    }
}
