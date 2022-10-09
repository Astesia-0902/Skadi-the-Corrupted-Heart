using Attackers;
using Defenders;
using Res.Scripts.Attackers;
using Res.Scripts.Defenders;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 单位的血条等UI
    /// </summary>
    public class UiForUnits : MonoBehaviour
    {
        [SerializeField] private bool defaultStatus;
        
        private Transform healthBarPivot;
        public Transform healthBarTransform;
        private Image healthSliderBar;
        public GameObject healthBarPrefeb;

        private Transform sanityBarPivot;
        private Transform sanityBarTransform;
        public GameObject sanityBarPrefeb;
        private Image sanitySliderBar;
        
        private Attacker attacker;
        private Defender defender;
        

        private void Awake()
        {
            healthBarPivot = transform.GetChild(0);
            sanityBarPivot = transform.GetChild(1);
            attacker = GetComponentInParent<Attacker>();
            defender = GetComponentInParent<Defender>();

            if (attacker != null)
            {
                attacker.onHealthChanged += UpdateHealthBar;
            }
            else if (defender != null)
            {
                defender.onHealthChanged += UpdateHealthBar;
                defender.onSanityChanged += UpdateSanity;
            }
        }

        private void OnEnable()
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                //在worldspace canvas下生成ui
                if (canvas.renderMode == RenderMode.WorldSpace && canvas.CompareTag("UI"))
                {
                    healthBarTransform = Instantiate(healthBarPrefeb, canvas.transform).transform;
                    canvas.worldCamera = Camera.main;
                    healthSliderBar = healthBarTransform.GetChild(0).GetComponent<Image>();
                    healthBarTransform.gameObject.SetActive(defaultStatus);//默认显示还是不显示

                    //如果当前单位是防守方，那么还要生成神经损伤的ui
                    if (defender != null)
                    {
                        sanityBarTransform = Instantiate(sanityBarPrefeb, canvas.transform).transform;
                        sanitySliderBar = sanityBarTransform.GetComponent<Image>();
                        sanityBarTransform.gameObject.SetActive(false);
                    }
                }
            }
        }

        
        private void Update()
        {
            if (attacker == null && defender == null)
            {
                
            }
            
            //UI需要持续跟随单位
            if (healthBarTransform != null)
            {
                healthBarTransform.position = healthBarPivot.position;
                healthBarTransform.forward = -Camera.main.transform.forward;
            }

            if (sanityBarTransform != null)
            {
                sanityBarTransform.position = sanityBarPivot.position;
                sanityBarTransform.forward = -Camera.main.transform.forward;
            }
        }

        #region Health Bar

        private void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            if (healthBarTransform != null && !healthBarTransform.gameObject.activeInHierarchy)
                healthBarTransform.gameObject.SetActive(true);
            
            if (currentHealth <= 0f)
            {
                DestroyBar();
                return;
            }

            float value = currentHealth / maxHealth;
            healthSliderBar.fillAmount = value;
        }

        public void DestroyBar()
        {
            if (healthBarTransform != null)
            {
                Destroy(healthBarTransform.gameObject);
            }

            if (sanityBarTransform != null)
            {
                Destroy(sanityBarTransform.gameObject);
            }
        }

        #endregion

        #region Sanity Bar

        private void UpdateSanity(float currentSanity)
        {
            if (sanityBarTransform != null && !sanityBarTransform.gameObject.activeInHierarchy)
                sanityBarTransform.gameObject.SetActive(true);

            sanitySliderBar.fillAmount = currentSanity / 1000f;
            
            if (currentSanity >= 1000f)
            {
                sanitySliderBar.color = new Color(1f, 1f, 1f, 1f);
                sanityBarTransform.gameObject.SetActive(false);
            }
            else if (currentSanity <= 0f)
            {
                sanityBarTransform.gameObject.SetActive(true);
                sanitySliderBar.color = new Color(1f, 1f, 1f, 0.7f);
            }
            else
            {
                sanityBarTransform.gameObject.SetActive(true);
            }
        }

        #endregion
        
    }
}