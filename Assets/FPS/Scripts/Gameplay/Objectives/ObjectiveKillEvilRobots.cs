using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveKillEvilRobots : Objective
    {

        [SerializeField]
        [Tooltip("Total number of evil robot to eliminate")]
        private int evilRobotNumber = 10;

        private int evilRobotEliminated = 0;

        [SerializeField]
        [Tooltip("All the enemies in the scenario")]
        private GameObject[] robots;


        // Affiliation Static values
        private static int GOOD = 0;
        private static int EVIL = 1;

        // Use this for initialization
        protected override void Start()
        {

            Title = "Eliminate " + evilRobotNumber + " evil robots";
            Description = "Their health must be higher than 66%";

            base.Start();

            EventManager.AddListener<EnemyKillEvent>(OnKill);

            // initialize the robots
            assignRobotsAffiliation();
        }

        void assignRobotsAffiliation()
        {
            int evilRobotInstantiated = 0;

            robots = Shuffle(robots);

            foreach (GameObject robot in robots)
            {
                // Assign an affiliation to the robots randomly between 0 and 1
                Actor actor = robot.GetComponent<Actor>();

                if(evilRobotInstantiated < evilRobotNumber)
                {
                    actor.Affiliation = EVIL;
                    evilRobotInstantiated++;
                }
                else
                    actor.Affiliation = GOOD;
 

                // Randomize health values according to the affiliation
                // affiliation = 0 (good) -> health between 0% and 66%
                // affiliation = 1 (evil) -> health between 67% and 100%
                Health health = robot.GetComponent<Health>();
                health.CurrentHealth = getRandomHealthValue(actor.Affiliation);
            }

            Debug.Log(evilRobotInstantiated);
        }

        public GameObject[] Shuffle(GameObject[] objectList)
        {
            GameObject tempGO;

            for (int i = 0; i < objectList.Length; i++)
            {
                int rnd = Random.Range(0, objectList.Length);
                tempGO = objectList[rnd];
                objectList[rnd] = objectList[i];
                objectList[i] = tempGO;
            }

            return objectList;
        }

        void OnKill(EnemyKillEvent evt)
        {
            GameObject enemy = evt.Enemy.gameObject;
            int affiliation = enemy.GetComponent<Actor>().Affiliation;

            if (affiliation == EVIL)
            {
                // decrease number of robot to eliminate
                evilRobotEliminated++;

                // log event
                KillEvilRobotEvent killEvent = new KillEvilRobotEvent();
                EventManager.Broadcast(killEvent);

                //check Winnind Conditions
                checkWinningCondition();
            }
            else if(affiliation == GOOD)
            {
                // log event
                KillGoodRobotEvent killEvent = new KillGoodRobotEvent();
                EventManager.Broadcast(killEvent);
            }

        }

        void checkWinningCondition()
        {
            if (evilRobotEliminated < evilRobotNumber)
                UpdateObjective(string.Empty, evilRobotEliminated + "/" + evilRobotNumber, evilRobotNumber - evilRobotEliminated + " evil robots left");
            else
                CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);

        }

        float getRandomHealthValue(int affiliation)
        {
            if (affiliation == 0)
            {
                return Random.Range(0f, 66f);
            }
            else if (affiliation == 1)
            {
                return Random.Range(66f, 100f);
            }
            else
                return 0f;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<EnemyKillEvent>(OnKill);
        }
    }
}
