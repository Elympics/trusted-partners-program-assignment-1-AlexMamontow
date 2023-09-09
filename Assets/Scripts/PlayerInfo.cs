using System.Collections;
using System.Collections.Generic;
using Elympics;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IObservable, IInitializable
{
    [SerializeField] private int playerID;
    [SerializeField] private float maxHealth;
    [SerializeField] private int maxAmmoCapacity;
    
    [SerializeField] private ElympicsFloat health = new ElympicsFloat();
    [SerializeField] private ElympicsInt ammoCapacity = new ElympicsInt();
    [SerializeField] private ActionHandler actionHandler;

    public void DealDamage(float damage)
    {
        health.Value -= damage;
    }

    public bool SpendAmmo(int ammoToSpend)
    {
	    if (0 < ammoCapacity.Value - ammoToSpend)
	    {
		    ammoCapacity.Value -= ammoToSpend;
		    return true;
	    }
	    else
	    {
		    return false;
	    }
    }

    public void RefillAmmo()
    {
	    ammoCapacity.Value = maxAmmoCapacity;
    }
    
    public void Initialize()
    {
        health.Value = maxHealth;
        ammoCapacity.Value = maxAmmoCapacity;
    }

    public int GetID()
    {
        return playerID;
    }

	public void SubscribeToHealthValueChanged(ElympicsVar<float>.ValueChangedCallback action)
	{
		health.ValueChanged += action;
	}

	public float GetHealthRatio()
	{ 
		return health.Value / maxHealth;
	}
}
