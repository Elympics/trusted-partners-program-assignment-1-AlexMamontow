using System.Collections;
using System.Collections.Generic;
using Elympics;
using UnityEngine;

public class ActionHandler : MonoBehaviour, IObservable
{
    [SerializeField] private AttackHandler attackHandler;
    [SerializeField] private ElympicsInt currentAction = new ElympicsInt();

    [SerializeField] private int attackCooldown;

    private long attackAvailableTick = 0;

    public int GetCurrentAction()
    {
        return currentAction.Value;
    }

    public void HandleActions(bool attack, long tick)
    {
        if (attack && tick > attackAvailableTick)
        {
            attackHandler.Attack(tick);
            attackAvailableTick = tick + attackCooldown;
        }
        
    }
}
