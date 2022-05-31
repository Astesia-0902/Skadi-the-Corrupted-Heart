using System;
using Attackers;
using Defenders;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private bool defaultStatus;
        private Transform uiPivot;
        private Transform uiBar;
        private Image sliderBar;
        private Attacker attacker;
        private Defender defender;
        public GameObject healthBarPrefeb;

        private void Awake()
        {
            uiPivot = transform.parent.GetChild(2);
            attacker = GetComponentInParent<Attacker>();
            defender = GetComponentInParent<Defender>();

            if (attacker != null)
            {
                Debug.Log("注册至:" + attacker);
                attacker.OnHealthChanged += UpdateHealthBar;
            }
            else if (defender != null)
            {
                Debug.Log("注册至:" + defender);
                defender.OnHealthChanged += UpdateHealthBar;
            }
        }

        private void OnEnable()
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.renderMode == RenderMode.WorldSpace && canvas.CompareTag("UI"))
                {
                    uiBar = Instantiate(healthBarPrefeb, canvas.transform).transform;
                    canvas.worldCamera = Camera.main;
                    sliderBar = uiBar.GetChild(0).GetComponent<Image>();
                    uiBar.gameObject.SetActive(defaultStatus);
                }
            }
        }

        private void Update()
        {
            if (uiBar != null)
            {
                uiBar.position = uiPivot.position;
                uiBar.forward = -Camera.main.transform.forward;
            }
        }

        private void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            if (uiBar != null && !uiBar.gameObject.activeInHierarchy)
                uiBar.gameObject.SetActive(true);
            
            if (currentHealth <= 0f)
            {
                DestroyBar();
                return;
            }

            float value = currentHealth / maxHealth;
            sliderBar.fillAmount = value;
        }

        public void DestroyBar()
        {
            if (uiBar != null)
            {
                Destroy(uiBar.gameObject);
            }
        }
    }
}