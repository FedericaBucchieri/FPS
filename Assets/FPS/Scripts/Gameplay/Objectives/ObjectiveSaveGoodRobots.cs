using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveSaveGoodRobots : Objective
    {
        [SerializeField]
        [Tooltip("Number of enemy that you could kill by mistake")]
        private int mistakesAllowed = 2;

        private int mistakesDone = 0;

        [SerializeField]
        [Tooltip("Player health reference")]
        private Health player;

        private List<GameObject> damagedByMistake = new List<GameObject>();

        // Affiliation Static values
        private static int GOOD = 0;

        // Start is called before the first frame update
        protected override void Start()
        {
            Title = "Ignore the Good Robots";
            Description = "Don't hit good robots more than " + mistakesAllowed + " times";

            // Always a Secondary Objective, paired with ObjectiveKillEvilRobots
            IsOptional = true;

            EventManager.AddListener<DamageEvent>(OnDamage);

            base.Start();
        }

        void OnDamage(DamageEvent evt)
        {

            if (evt.enemyAffiliation == GOOD)
            {
                Debug.Log(!damagedByMistake.Contains(evt.EnemyDamaged));
                if (!damagedByMistake.Contains(evt.EnemyDamaged))
                {
                    damagedByMistake.Add(evt.EnemyDamaged);
                    mistakesDone++;

                    UpdateObjective(string.Empty, mistakesDone + "/" + mistakesAllowed, "You can hit a good robot " + mistakesAllowed + " time left");

                    if (mistakesDone == mistakesAllowed)
                        player.Kill();
                }
            }
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<DamageEvent>(OnDamage);
        }
    }
}
