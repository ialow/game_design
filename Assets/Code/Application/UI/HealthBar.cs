using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Ddd.Domain;

namespace Ddd.Application
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private float TimeDamageEffect = 0.3f;
        [SerializeField] private float TimeDelayEffect = 0.15f;
        [SerializeField] private Slider sliderEffects;

        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            Player.HealthBarEvent += HandlerHealthBar;
        }

        private void HandlerHealthBar(float currentHealth)
        {
            //if (currentHealth == 0)
            //{
            //    slider.value = 0;
            //    sliderEffects.value = 0;
            //    return;
            //}

            slider.value = currentHealth;
            DamageEffect(currentHealth);
        }

        private void DamageEffect(float targetValue)
        {
            sliderEffects.DOKill();
            sliderEffects.DOValue(targetValue, TimeDamageEffect).SetDelay(TimeDelayEffect);
        }
    }
}