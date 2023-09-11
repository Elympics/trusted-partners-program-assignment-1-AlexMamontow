using System.Collections;
using System.Collections.Generic;
using TMPro;
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
	[SerializeField] 
	private TextMeshProUGUI ammoText;

	private void Awake()
	{
		playerinfo.SubscribeToHealthValueChanged(DisplayHealthChangeEffects);
		playerinfo.SubscribeToAmmoValueChanged(DisplayAmmoChangeEffects);
	}

	private void DisplayHealthChangeEffects(float oldValue, float newValue)
	{
		hpSlider.value = playerinfo.GetHealthRatio();

		if (newValue < oldValue)
		{
			damagedEffect.Play();
		}
	}

	private void DisplayAmmoChangeEffects(int oldValue, int newValue)
	{
		ammoText.text = "Ammo: " + newValue.ToString() + "/" + playerinfo.GetMaxAmmoCapacity().ToString();
	}


}
