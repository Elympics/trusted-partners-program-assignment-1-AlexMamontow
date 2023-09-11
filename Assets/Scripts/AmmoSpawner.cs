using System.Collections;
using System.Collections.Generic;
using Elympics;
using UnityEngine;

public class AmmoSpawner : ElympicsMonoBehaviour, IUpdatable
{
    private bool isSpawned = false;
    private ElympicsBool spawnActive = new ElympicsBool();


    [SerializeField] private int minCooldown = 50;
    [SerializeField] private int maxCooldown = 200;
    [SerializeField] private long cooldownTick;
        
    [SerializeField] private Transform spawnPosition;
    private AmmoConsumable spawnedConsumable;
    
    [SerializeField] private GameObject ammoConsumablePrefab;

    void Start()
    {
        cooldownTick = Elympics.Tick + Random.Range(minCooldown, maxCooldown);
    }
    private AmmoConsumable CreateAmmoConsumable()
    {
        AmmoConsumable ammoConsumable = ElympicsInstantiate(ammoConsumablePrefab.name, ElympicsPlayer.All).GetComponent<AmmoConsumable>();
        ammoConsumable.SetSpawner(this);
        ammoConsumable.transform.position = spawnPosition.position;

        isSpawned = true;
        
        return ammoConsumable;
    }

    public void RemoveConsumable()
    {
        isSpawned = false;
        cooldownTick = Elympics.Tick + Random.Range(minCooldown, maxCooldown);
    }

    public AmmoConsumable GetConsumable()
    {
        return spawnedConsumable;
    }

    public void ChangeSpawnAbility(bool value)
    {
        spawnActive.Value = value;
        cooldownTick = Elympics.Tick + Random.Range(minCooldown, maxCooldown);
    }

    public void ElympicsUpdate()
    {
        if (spawnActive.Value && !isSpawned && Elympics.Tick > cooldownTick)
        {
            spawnedConsumable = CreateAmmoConsumable();
            cooldownTick = Elympics.Tick + Random.Range(minCooldown, maxCooldown);
        }
        else if(!spawnActive.Value)
        {
            isSpawned = false;
            spawnedConsumable?.SetDestroyTrigger(true);
        }
    }
}
