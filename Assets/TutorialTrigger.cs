using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{

    public TutorialManager tutorialManager;

    private void OnTriggerEnter(Collider other)
    {
        tutorialManager.nextTutorialRule();
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(this.gameObject);
    }
}
