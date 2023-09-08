using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEffectHandler : MonoBehaviour
{
	[SerializeField]
	private PlayerInfo playerinfo;
	[SerializeField]
	private Slider hpSlider;
	[SerializeField]
	private ParticleSystem damagedEffect;

	private void Awake()
	{
		playerinfo.SubscribeToHealthValueChanged(DisplayHealthChangeEffects);
	}

	private void DisplayHealthChangeEffects(float oldValue, float newValue)
	{
		hpSlider.value = playerinfo.GetHealthRatio();

		if (newValue < oldValue)
		{
			damagedEffect.Play();
		}
	}


}
