using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class WorldspaceHealthBar : MonoBehaviour
    {
        [Tooltip("Health component to track")] public Health Health;

        [Tooltip("Image component displaying health left")]
        public Image HealthBarImage;

        [Tooltip("The floating healthbar pivot transform")]
        public Transform HealthBarPivot;

        [Tooltip("Whether the health bar is visible when at full health or not")]
        public bool HideFullHealthBar = true;



        void Update()
        {
            // update health bar value
            float healthPercentage = Health.CurrentHealth / Health.MaxHealth;
            HealthBarImage.fillAmount = healthPercentage;

            if (healthPercentage < 0.3)
            {
                HealthBarImage.color = Color.red;
            }
            else if (healthPercentage > 0.3 && healthPercentage < 0.6)
            {
                HealthBarImage.color = Color.yellow;
            }
            else
            {
                HealthBarImage.color = Color.green;
            }

            // rotate health bar to face the camera/player
            HealthBarPivot.LookAt(Camera.main.transform.position);

            // hide health bar if needed
            if (HideFullHealthBar)
                HealthBarPivot.gameObject.SetActive(HealthBarImage.fillAmount != 1);
        }
    }
}