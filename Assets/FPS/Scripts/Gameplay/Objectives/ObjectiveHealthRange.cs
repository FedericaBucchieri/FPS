using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveHealthRange : Objective
    {
        [Tooltip("List of enemies")]
        public GameObject[] enemies;

        [Tooltip("Upperbound of the first range of the enemies' health ranges categories")]
        public int firstRangeUp;

        [Tooltip("Upperbound of the second range of the enemies' health ranges categories")]
        public int secondRangeUp;

        private int firstRangeNumber;
        private int secondRangeNumber;
        private int thirdRangeNumber;

        private int firstRangeGoal;
        private int secondRangeGoal;
        private int thirdRangeGoal;


        protected override void Start()
        {
            base.Start();

            EventManager.AddListener<EnemyHitEvent>(OnEnemyHit);

            // set a title and description specific for this type of objective, if it hasn't one
            if (string.IsNullOrEmpty(Title))
                Title = "Hit the enemies untile the desired health ranges";

            if (string.IsNullOrEmpty(Description))
                Description = "";

            // Generate the Ranges Goals Randomly
            int enemiesNumber = enemies.Length;

            firstRangeGoal = Random.Range(1, enemiesNumber - 3);
            secondRangeGoal = Random.Range(1, enemiesNumber - firstRangeGoal - 3);
            thirdRangeGoal = Random.Range(1, enemiesNumber - firstRangeGoal - secondRangeGoal - 3);
        }

        void OnEnemyHit(EnemyHitEvent evt)
        {
            if (IsCompleted)
                return;

            if(evt.HealthLevel < firstRangeUp)
            {
                firstRangeNumber++;
            }
            else if(evt.HealthLevel > firstRangeUp && evt.HealthLevel < secondRangeUp)
            {
                secondRangeNumber++;
            }
            else if(evt.HealthLevel > secondRangeUp)
            {
                thirdRangeNumber++;
            }

            if(firstRangeNumber == firstRangeGoal && secondRangeNumber == secondRangeGoal && thirdRangeNumber == thirdRangeGoal)
                CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);

        }

        void OnDestroy()
        {
            EventManager.RemoveListener<EnemyHitEvent>(OnEnemyHit);
        }
    }
}