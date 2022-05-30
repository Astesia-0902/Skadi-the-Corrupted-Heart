using System;
using Attackers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        private Transform uiPivot;
        private Transform uiBar;
        private Image sliderBar;
        private Attacker attacker;
        public GameObject healthBarPrefeb;

        private void Awake()
        {
            uiPivot = transform.GetChild(2);
            attacker = GetComponent<Attacker>();
            attacker.OnHealthChanged += UpdateHealthBar;
        }

        private void OnEnable()
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.renderMode == RenderMode.WorldSpace && canvas.CompareTag("UI"))
                {
                    uiBar = Instantiate(healthBarPrefeb, uiPivot).transform;
                    canvas.worldCamera = Camera.main;
                    sliderBar = uiBar.GetChild(0).GetComponent<Image>();
                    uiBar.gameObject.SetActive(false);
                }
            }
        }

        private void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            if (uiBar != null && !uiBar.gameObject.activeInHierarchy)
                uiBar.gameObject.SetActive(true);
            
            if (currentHealth <= 0f)
            {
                uiBar.gameObject.SetActive(false);
                return;
            }

            float value = currentHealth / maxHealth;
            sliderBar.fillAmount = value;
        }
    }
}