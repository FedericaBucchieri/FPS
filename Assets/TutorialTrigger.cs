using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{

    [SerializeField]
    TutorialManager tutorialManager;

    [SerializeField]
    int tutorialIndex;
    

    private void OnTriggerEnter(Collider other)
    {
        tutorialManager.DisplayTutorialRule(tutorialIndex);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(this.gameObject);
    }
}
