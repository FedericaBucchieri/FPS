using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class TrainingManager : MonoBehaviour
    {
        private Transform respawnPoint;

        [SerializeField]
        private GameObject quitUI;

        [SerializeField]
        private GameObject goodButton;

        [SerializeField]
        private GameObject evilButton;

        [SerializeField]
        private GameObject correctUI;

        [SerializeField]
        private GameObject wrongUI;

        [SerializeField]
        private Text healthValue;

        [SerializeField]
        private GameObject healthValueUI;

        [SerializeField]
        private GameObject nextButton;

        [SerializeField]
        private GameObject enemyPrefab;

        [SerializeField]
        private int minTrainingNumber = 10;

        int trainingDone = 0;
        GameObject currentEnemy;

         void Start()
        {

            // set the respawn point 
            respawnPoint = this.transform;

            EventManager.AddListener<TrainingEvent>(OnTrainingEvent);

            // instantiate the first enemy
            currentEnemy = Instantiate(enemyPrefab, respawnPoint.position, Quaternion.identity);
            // initialize buttons
            goodButton.SetActive(true);
            evilButton.SetActive(true);
            healthValueUI.SetActive(false);
        }

        void OnTrainingEvent(TrainingEvent evt)
        {
            // update conting
            trainingDone++;
            // initialize buttons
            goodButton.SetActive(false);
            evilButton.SetActive(false);


            // validate answer
            if(currentEnemy != null)
            {
                Health currentEnemyHealth = currentEnemy.GetComponent<Health>();

                if ((evt.evil && currentEnemyHealth.CurrentHealth > 66f) || (!evt.evil && currentEnemyHealth.CurrentHealth < 66f))
                    correctUI.SetActive(true);
                else
                    wrongUI.SetActive(true);

                healthValueUI.SetActive(true);
                healthValue.text = " Health Value: " + (int)currentEnemyHealth.CurrentHealth + "%";
                nextButton.SetActive(true);


                if (trainingDone >= minTrainingNumber)
                    quitUI.SetActive(true);
            }

            
        }

        public void DisplayNextTraining()
        {
            // destroy current enemy
            //Destroy(currentEnemy.gameObject);

            currentEnemy.gameObject.SetActive(false);

            // reset UI
            healthValueUI.SetActive(false);
            correctUI.SetActive(false);
            wrongUI.SetActive(false);
            nextButton.SetActive(false);

            // instantiate the next enemy
            currentEnemy = Instantiate(enemyPrefab, respawnPoint.position, Quaternion.identity);
            currentEnemy.gameObject.SetActive(true);
            // initialize buttons
            goodButton.SetActive(true);
            evilButton.SetActive(true);
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<TrainingEvent>(OnTrainingEvent);
        }

    }
}
