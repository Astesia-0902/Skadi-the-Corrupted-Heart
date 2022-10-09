using System.Collections;
using System.Collections.Generic;
using Res.Scripts.Game_Managers;
using UnityEngine;
using UnityEngine.UI;
using Tool_Scripts;

public class TopBarUI : Singleton<TopBarUI>
{
    public GameObject SkeletonAnimation;
    public GameObject eff_Saoguang;
    public AudioSource lightHouseAudio;
    private RectTransform lighthouseMask;
    private Scrollbar seabornHP;

    private bool lightHouseOn = false;
    private float originalWidth;
    private float originalHeight;

    private float health = 0;
    private float maxHealth = 0;

    private void Start()
    {
        lighthouseMask = transform.Find("lighthouseMask").GetComponent<RectTransform>();
        seabornHP = transform.Find("SeabornHP").GetComponent<Scrollbar>();
        originalWidth = lighthouseMask.rect.width;
        originalHeight = lighthouseMask.rect.height;
    }

    private void Update()
    {
        LightHouseProgressBarcheck();
        AudioAndLightCheck();
    }

    private void LightHouseProgressBarcheck()
    {
        if (!TimelineManager.Instance.lighthouseFlag)
        {
            lighthouseMask.sizeDelta = new Vector2(originalWidth, (1 - ((float)TimelineManager.Instance.lighthouseTimer / 60)) * originalHeight);
        }
        SkeletonAnimation.SetActive(TimelineManager.Instance.lighthouseFlag);
    }

    private void AudioAndLightCheck()
    {
        if (TimelineManager.Instance.lighthouseFlag && !lightHouseOn)
        {
            lightHouseOn = true;
            lightHouseAudio.Play();
            eff_Saoguang.SetActive(true);
        }
        else if (!TimelineManager.Instance.lighthouseFlag)
        {
            eff_Saoguang.SetActive(false);
            lightHouseOn = false;
        }
    }

    public void IconMovement(float currentHealth ,float maxHealthOfSeaborn)
    {
        if (maxHealthOfSeaborn <= 0)
            return;

        if(0 == maxHealth)
        {
            maxHealth = maxHealthOfSeaborn;
            health = maxHealth;
        }

        if (0 == health)
        {
            health = maxHealth;
        }

        if (health > currentHealth)
        {
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }

            float difHealth = health - currentHealth;

            health = currentHealth;

            seabornHP.size -= difHealth / (maxHealth * 4);
        }
    }
}
