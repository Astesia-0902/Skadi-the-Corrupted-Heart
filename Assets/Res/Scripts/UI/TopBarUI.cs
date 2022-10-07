using System.Collections;
using System.Collections.Generic;
using Res.Scripts.Game_Managers;
using UnityEngine;
using UnityEngine.UI;
using Tool_Scripts;

public class TopBarUI : Singleton<TopBarUI>
{

    private RectTransform lighthouseMask;
    public GameObject SkeletonAnimation;
    private Scrollbar seabornHP;
    public AudioSource lightHouseAudio;
    private bool audioPlayed = false;
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
        LightHouseAudioCheck();
    }

    private void LightHouseProgressBarcheck()
    {
        if (!TimelineManager.Instance.lighthouseFlag)
        {
            lighthouseMask.sizeDelta = new Vector2(originalWidth, (1 - ((float)TimelineManager.Instance.lighthouseTimer / 60)) * originalHeight);
        }
        SkeletonAnimation.SetActive(TimelineManager.Instance.lighthouseFlag);
    }

    private void LightHouseAudioCheck()
    {
        if (TimelineManager.Instance.lighthouseFlag && !audioPlayed)
        {
            audioPlayed = true;
            lightHouseAudio.Play();
        }
        else if (!TimelineManager.Instance.lighthouseFlag)
            audioPlayed = false;
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
