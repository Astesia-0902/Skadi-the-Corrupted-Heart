using System.Collections;
using Tool_Scripts;
using UnityEngine;

namespace Res.Scripts.Game_Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioSource fxSource;
        private AudioSource bgmSource;

        protected override void Awake()
        {
            base.Awake();
            if (Camera.main != null)
            {
                AudioSource[] audioSources = Camera.main.GetComponents<AudioSource>();
                fxSource = audioSources[0];
                fxSource.volume = 0.3f;
                bgmSource = audioSources[1];
            }
        }


        public void PlayTargetClip(AudioClip clip)
        {
            fxSource.PlayOneShot(clip);
        }

        public void PlayTargetBGM(AudioClip bgmClip)
        {
            bgmSource.volume = 1f;
            bgmSource.loop = true;
            bgmSource.Play();
            //StartCoroutine(PlayTargetMusicIE(bgmClip));
        }

        private IEnumerator PlayTargetMusicIE(AudioClip bgmClip)
        {
            yield return BGMFadeOut();
            bgmSource.clip = bgmClip;
            bgmSource.volume = 1f;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        private IEnumerator BGMFadeOut()
        {
            while (bgmSource.volume > 0)
            {
                bgmSource.volume -= Time.deltaTime;
                yield return null;
            }
        }
    }
}