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

        public Color green;
        public Color orange;
        public Color red;

        void Update()
        {
            // update health bar value
            HealthBarImage.fillAmount = Health.CurrentHealth / Health.MaxHealth;
            
            if (HealthBarImage.fillAmount > 0.3 && HealthBarImage.fillAmount < 0.6)
            {
                HealthBarImage.color = new Color(orange.r, orange.g, orange.b, 1f);
            }
            else if (HealthBarImage.fillAmount < 0.3)
            {
                HealthBarImage.color = new Color(red.r, red.g, red.b, 1f);
            }

            // rotate health bar to face the camera/player
            HealthBarPivot.LookAt(Camera.main.transform.position);

            // hide health bar if needed
            if (HideFullHealthBar)
                HealthBarPivot.gameObject.SetActive(HealthBarImage.fillAmount != 1);
        }
    }
}