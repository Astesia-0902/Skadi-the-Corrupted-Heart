using System;
using UnityEngine;

namespace Tool_Scripts
{
    /// <summary>
    /// 单例模式的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get { return _instance; }
        }

        protected void Awake()
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = (T) this;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}