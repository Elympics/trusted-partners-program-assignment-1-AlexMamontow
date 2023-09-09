using System;
using System.Collections;
using System.Collections.Generic;
using Elympics;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : ElympicsMonoBehaviour, IUpdatable
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody bulletRigidbody;
    private PlayerInfo playerInfo;
    private float bulletDamage;
    private ElympicsBool shouldBeDestroyed = new ElympicsBool();

    public void SetUpBullet(Vector3 position, Quaternion rotation, PlayerInfo info, float damage, Vector3 direction)
    {
        transform.position = position;
        transform.rotation = rotation;
        playerInfo = info;
        bulletDamage = damage;

		bulletRigidbody.velocity = direction.normalized * speed;

	}

    public void SetTick(long tick)
    {
        
    }

    private void OnDisable()
    {
        bulletRigidbody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInfo collidedPlayer = other.GetComponentInChildren<PlayerInfo>();
        if (null != collidedPlayer && playerInfo != collidedPlayer)
        {
            collidedPlayer.DealDamage(bulletDamage);
            shouldBeDestroyed.Value = true;
        } 
        else if (null == collidedPlayer)
        {
            shouldBeDestroyed.Value = true;
        }
    }

    public void ElympicsUpdate()
    {
        if(shouldBeDestroyed.Value) ElympicsDestroy(this.gameObject);
    }
}
