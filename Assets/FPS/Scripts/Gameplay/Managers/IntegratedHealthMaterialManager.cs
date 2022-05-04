using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Game
{
    public class IntegratedHealthMaterialManager : MonoBehaviour
    {
        [Tooltip("Health component to track")]
        public Health Health;

        public Material redMaterial;
        public Material yellowMaterial;
        public Material greenMaterial;

        public GameObject[] robotParts;

        public GameObject[] cylinders;
        public float[] healthIntervals;

        private int index;

        private void Start()
        {
            index = cylinders.Length;
        }

        void Update()
        {
            // update health bar value
            float healthPercentage = Health.CurrentHealth / Health.MaxHealth;

            foreach(GameObject robotPart in robotParts)
            {
                if (healthPercentage < 0.3)
                {
                    robotPart.GetComponent<SkinnedMeshRenderer>().material = redMaterial;
                }
                else if (healthPercentage > 0.3 && healthPercentage < 0.6)
                {
                    robotPart.GetComponent<SkinnedMeshRenderer>().material = yellowMaterial;
                }
                else
                {
                    robotPart.GetComponent<SkinnedMeshRenderer>().material = greenMaterial;
                }
            }


            for(int i = 0; i< cylinders.Length; i++)
            {
                if(healthPercentage <= healthIntervals[i])
                    cylinders[i].SetActive(false);
                else
                    cylinders[i].SetActive(true);
            }

        }
    }
}
