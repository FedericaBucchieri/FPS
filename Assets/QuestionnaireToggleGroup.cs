using UnityEngine;
using Unity.FPS.Game;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class QuestionnaireToggleGroup : MonoBehaviour
{
    public int questionIndex;

    public string getAnswer()
    {
        Toggle activeToggle = GetComponent<ToggleGroup>().GetFirstActiveToggle();
        Debug.Log(activeToggle.gameObject.name);
        return activeToggle.name;
    }

}
