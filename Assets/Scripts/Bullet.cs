using System;
using System.Collections;
using System.Collections.Generic;
using Elympics;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : ElympicsMonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rigidbody;
    private IObjectPool<Bullet> pool;
    private PlayerInfo playerInfo;
    private float bulletDamage;

    public void SetPool(IObjectPool<Bullet> pool)
    {
        pool = pool;
    }

    public void SetUpBullet(Vector3 position, Quaternion rotation, PlayerInfo info, float damage)
    {
        transform.position = position;
        transform.rotation = rotation;
        playerInfo = info;
        bulletDamage = damage;
    }

    public void SetTick(long tick)
    {
        
    }

    private void OnBecameInvisible()
    {
        pool?.Release(this);
    }

    private void OnEnable()
    {
        rigidbody.velocity = Vector3.right * speed;
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector3.zero;
    }
}
