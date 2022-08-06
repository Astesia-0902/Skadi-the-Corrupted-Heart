using System;
using Res.Scripts.Attackers;
using UnityEngine;

namespace Res.Scripts.Defenders._Thorns
{
    public class ThornsNeuralDamageEffect : MonoBehaviour
    {
        private Attacker currentTarget;
        public int timer;
        private float timerHelper;

        private void Awake()
        {
            currentTarget = GetComponentInParent<Attacker>();
        }

        private void OnEnable()
        {
            ThornsNeuralDamageEffect[] thornsNeuralDamageEffects =
                currentTarget.GetComponentsInChildren<ThornsNeuralDamageEffect>();
            
            if (thornsNeuralDamageEffects.Length > 1)
            {
                thornsNeuralDamageEffects[0].timer = 0;
                Destroy(this.gameObject);
            }
        }

        private void Update()
        {
            timerHelper += Time.deltaTime;
            if (timerHelper >= 1f)
            {
                timerHelper = 0f;
                timer++;
                currentTarget.TakeDamage(0, currentTarget.isRange ? 280 : 140, 0);
            }

            if (timer >= 3)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
