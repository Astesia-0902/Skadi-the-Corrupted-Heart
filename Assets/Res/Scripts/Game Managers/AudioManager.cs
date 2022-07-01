using System;
using System.Collections;
using Tool_Scripts;
using UnityEngine;

namespace Game_Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioSource fxSource;
        private AudioSource bgmSource;

        protected override void Awake()
        {
            base.Awake();
            fxSource = GetComponent<AudioSource>();
            bgmSource = GetComponentInParent<AudioSource>();
        }
        

        public void PlayTargetClip(AudioClip clip)
        {
            fxSource.PlayOneShot(clip);
        }

        public void PlayTargetBGM(AudioClip bgmClip)
        {
            StartCoroutine(PlayTargetMusicIE(bgmClip));
        }

        private IEnumerator PlayTargetMusicIE(AudioClip bgmClip)
        {
            yield return BGMFadeOut();
            bgmSource.clip = bgmClip;
            bgmSource.volume = 1f;
            bgmSource.Play();
        }

        private IEnumerator BGMFadeOut()
        {
            while (bgmSource.volume>0)
            {
                bgmSource.volume -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
