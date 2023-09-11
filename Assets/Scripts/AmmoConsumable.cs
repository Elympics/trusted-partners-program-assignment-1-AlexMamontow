using System.Collections;
using System.Collections.Generic;
using Elympics;
using UnityEngine;

public class AmmoConsumable : ElympicsMonoBehaviour, IUpdatable
{
    private ElympicsBool shouldBeDestroyed = new ElympicsBool();

    private AmmoSpawner spawner;

    public void SetSpawner(AmmoSpawner ammoSpawner)
    {
        spawner = ammoSpawner;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerInfo collidedPlayer = other.GetComponentInChildren<PlayerInfo>();
        if (null != collidedPlayer)
        {
            collidedPlayer.RefillAmmo();
            shouldBeDestroyed.Value = true;
            spawner?.RemoveConsumable();
        }
    }

    public void SetDestroyTrigger(bool value)
    {
        shouldBeDestroyed.Value = value;
    }
    
    public void ElympicsUpdate()
    {
        if(shouldBeDestroyed.Value) ElympicsDestroy(this.gameObject);
    }
}
