using System.Collections;
using System.Collections.Generic;
using Res.Scripts.Game_Managers;
using UnityEngine;
using Tool_Scripts;

public class TopBarUI : Singleton<TopBarUI>
{

    private RectTransform lighthouseMask;
    private RectTransform seabornIcon;
    public GameObject SkeletonAnimation;
    private float originalWidth;
    private float originalHeight;

    //С����Ѫ������ͼ���ƶ�
    private float health = 0;
    private float maxHealth = 0;

    private void Start()
    {
        lighthouseMask = transform.Find("lighthouseMask").GetComponent<RectTransform>();
        seabornIcon = transform.Find("seabornIcon").GetComponent<RectTransform>();
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
            Debug.Log("���Ѫ��" + maxHealth);
        }

        if (0 == health)
        {
            health = maxHealth;

            Debug.Log("��ǰѪ��Ϊ0��ˢ������Ѫ" + health);
        }

        if (health > currentHealth)
        {
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }

            float difHealth = health - currentHealth;
            
            Debug.Log("health:" + health + "--currentHealth:" + currentHealth);

            health = currentHealth;

            seabornIcon.anchoredPosition3D = new Vector3(seabornIcon.anchoredPosition3D.x + difHealth / maxHealth * 70, seabornIcon.anchoredPosition3D.y, seabornIcon.anchoredPosition3D.z);
        }
    }

    /*�����ű������޸ģ�
     LittleSeaborn.cs:32��-����IconMovement
    TimelineManager.cs��17��-�޸�Ϊpublic*/
}
