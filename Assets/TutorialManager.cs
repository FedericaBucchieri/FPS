using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using Unity.FPS.UI;
using UnityEngine.SceneManagement;


    public class TutorialManager : MonoBehaviour
    {
        [Tooltip("Set of instruction messages to display as popUps")]
        public GameObject[] popUps;
        [Tooltip("Final popUp Message")]
        public GameObject end_popUp;
        [Tooltip("UI rectangle where to display the messages")]
        public UITable DisplayMessageRect;
        [Tooltip("Player Object")]
        public GameObject player;


        private int popUpIndex = 0;
        PlayerInputHandler m_InputHandler;
        PlayerCharacterController m_CharacterController;
        PlayerWeaponsManager m_WeaponManager;
        private TutorialNotification currentPopUp;


        private void Start()
        {
            // get input handler
            m_InputHandler = player.GetComponent<PlayerInputHandler>();
            m_CharacterController = player.GetComponent<PlayerCharacterController>();
            m_WeaponManager = player.GetComponent<PlayerWeaponsManager>();

            // set the first instruction as the current one
            currentPopUp = popUps[popUpIndex].GetComponent<TutorialNotification>();
            currentPopUp.Initialized = true;
            DisplayMessageRect.UpdateTable(currentPopUp.gameObject);
        }

        private void Update()
        {
            switch (popUpIndex)
            {
                case 0: // movement tutorial
                    Vector3 worldspaceMoveInput = m_CharacterController.transform.TransformVector(m_InputHandler.GetMoveInput());
                    if (worldspaceMoveInput != Vector3.zero) // if the player moved
                        currentPopUp.endTrigger = true;
                    break;
                case 1: // Shooting tutorial
                    if (m_InputHandler.GetFireInputDown())
                        currentPopUp.endTrigger = true;
                    break;
                case 2: // Jump tutorial
                    if (m_InputHandler.GetJumpInputDown())
                        currentPopUp.endTrigger = true;
                    break;
                case 3: // Aim tutorial
                    if (m_InputHandler.GetAimInputHeld())
                    {
                        currentPopUp.endTrigger = true;
                        // ending Tutorial
                        endTutorial();
                    }
                    break;
                case 4: //exit Tutorial
                    endTutorial();
                    break;
                /*

            case 1: // Sprint tutorial
                    if (m_InputHandler.GetSprintInputHeld())
                        currentPopUp.endTrigger = true;
                    break;
                case 2: // Jump tutorial
                    if (m_InputHandler.GetJumpInputDown())
                        currentPopUp.endTrigger = true;
                    break;
                case 3: // Crouch tutorial
                    if (m_InputHandler.GetCrouchInputDown())
                        currentPopUp.endTrigger = true;
                    break;
                case 4: // Pickup tutorial
                    if (m_WeaponManager.GetActiveWeapon() != null)
                        currentPopUp.endTrigger = true;
                    break;
                case 5: // Shooting tutorial
                    if (m_InputHandler.GetFireInputDown())
                        currentPopUp.endTrigger = true;
                    break;
                case 6: // Change Weapon tutorial
                    if (m_InputHandler.GetSwitchWeaponInput() != 0) // To fix
                        currentPopUp.endTrigger = true;
                    break;
                case 7: // Aim tutorial
                    if (m_InputHandler.GetAimInputHeld())
                    {
                        currentPopUp.endTrigger = true;
                        // ending Tutorial
                        endTutorial();
                    }
                    break;
                */
        }
    }

        void endTutorial()
        {
            TutorialNotification end_popUp_notification = end_popUp.GetComponent<TutorialNotification>();
            end_popUp_notification.Initialized = true;
            DisplayMessageRect.UpdateTable(end_popUp_notification.gameObject);

            StartCoroutine(PrepareSceneChange());
        }

        IEnumerator PrepareSceneChange()
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(5);

            SceneManager.LoadScene("IntroductionTraining");
        }

        public void DisplayTutorialRule(int index)
    {
        popUpIndex = index;
        currentPopUp = popUps[popUpIndex].GetComponent<TutorialNotification>();
        currentPopUp.Initialized = true;
        DisplayMessageRect.UpdateTable(currentPopUp.gameObject);
    }

    /*
        public void nextTutorialRule()
        {
            popUpIndex++;
            currentPopUp = popUps[popUpIndex].GetComponent<TutorialNotification>();
            currentPopUp.Initialized = true;
            DisplayMessageRect.UpdateTable(currentPopUp.gameObject);
        }
    */
}
