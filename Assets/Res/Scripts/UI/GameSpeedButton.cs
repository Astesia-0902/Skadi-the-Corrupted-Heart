using System;
using Game_Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Res.Scripts.UI
{
    public class GameSpeedButton : MonoBehaviour
    {
        private Button button2X;
        private Button button1X;

        private void Awake()
        {
            button2X = transform.GetChild(0).GetComponent<Button>();
            button1X = transform.GetChild(1).GetComponent<Button>();
            button2X.onClick.AddListener(Button2XDown);
            button1X.onClick.AddListener(Button1XDown);
        }

        private void Start()
        {
            button2X.gameObject.SetActive(false);
            button1X.gameObject.SetActive(true);
        }

        private void Button2XDown()
        {
            Time.timeScale = 1f;
            button2X.gameObject.SetActive(false);
            button1X.gameObject.SetActive(true);
        }

        private void Button1XDown()
        {
            Time.timeScale = 2f;
            button2X.gameObject.SetActive(true);
            button1X.gameObject.SetActive(false);
        }
    }
}