using System;
using Res.Scripts.Game_Managers;
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
            GameManager.Instance.gameSpeed = 1f;
            Time.timeScale = GameManager.Instance.gameSpeed;
            button2X.gameObject.SetActive(false);
            button1X.gameObject.SetActive(true);
        }

        private void Button1XDown()
        {
            GameManager.Instance.gameSpeed = 2f;
            Time.timeScale = GameManager.Instance.gameSpeed;
            button2X.gameObject.SetActive(true);
            button1X.gameObject.SetActive(false);
        }
    }
}