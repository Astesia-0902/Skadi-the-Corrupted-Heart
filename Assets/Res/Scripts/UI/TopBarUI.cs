using System.Collections;
using System.Collections.Generic;
using Res.Scripts.Game_Managers;
using UnityEngine;
using UnityEngine.UI;
using Tool_Scripts;

public class TopBarUI : Singleton<TopBarUI>
{

    private RectTransform lighthouseMask;
    private RectTransform seabornIcon;
    public GameObject SkeletonAnimation;
    private Scrollbar seabornHP;
    private float originalWidth;
    private float originalHeight;

    //小海嗣血量关联图标移动
    private float health = 0;
    private float maxHealth = 0;

    private void Start()
    {
        lighthouseMask = transform.Find("lighthouseMask").GetComponent<RectTransform>();
        seabornIcon = transform.Find("seabornIcon").GetComponent<RectTransform>();
        seabornHP = transform.Find("SeabornHP").GetComponent<Scrollbar>();
        originalWidth = lighthouseMask.rect.width;
        originalHeight = lighthouseMask.rect.height;
    }

    private void Update()
    {
        if(!TimelineManager.Instance.lighthouseFlag)
        {
            lighthouseMask.sizeDelta = new Vector2(originalWidth, (1 - ((float)TimelineManager.Instance.lighthouseTimer / 60)) * originalHeight);
        }
        SkeletonAnimation.SetActive(TimelineManager.Instance.lighthouseFlag);
    }

    public void IconMovement(float currentHealth ,float maxHealthOfSeaborn)
    {
        if (maxHealthOfSeaborn <= 0)
            return;

        if(0 == maxHealth)
        {
            maxHealth = maxHealthOfSeaborn;
            health = maxHealth;
            Debug.Log("最高血量" + maxHealth);
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

    /*其他脚本内容修改：
     LittleSeaborn.cs:32行-调用IconMovement
    TimelineManager.cs：17行-修改为public*/
}
