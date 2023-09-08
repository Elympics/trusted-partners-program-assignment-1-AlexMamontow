using System.Collections;
using System.Collections.Generic;
using Elympics;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IObservable, IInitializable
{
    [SerializeField] private int playerID;
    [SerializeField] private float maxHealth;
    [SerializeField] private ElympicsFloat health = new ElympicsFloat();
    [SerializeField] private ActionHandler actionHandler;

    public void DealDamage(float damage)
    {
        health.Value -= damage;
    }
    public void Initialize()
    {
        health.Value = maxHealth;
    }

    public int GetID()
    {
        return playerID;
    }
}
