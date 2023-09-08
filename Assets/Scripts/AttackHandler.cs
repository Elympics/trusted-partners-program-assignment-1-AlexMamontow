using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using UnityEngine.Pool;

public class AttackHandler : ElympicsMonoBehaviour, IObservable
{
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private Transform BulletSpawnPoint;

    [SerializeField] private float bulletDamage;
    public void Attack(long tick)
    {
        Bullet bullet = m_bulletPool?.Get();
        bullet.SetTick(tick);
    } 
    
    [SerializeField] private GameObject bulletPrefab;

    private IObjectPool<Bullet> m_bulletPool;

    private void Awake()
    {
        m_bulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet,
            maxSize: 5);
    }


    private Bullet CreateBullet()
    {

        Bullet bullet = ElympicsInstantiate(bulletPrefab.name, ElympicsPlayer.World).GetComponent<Bullet>();
        bullet.SetPool(m_bulletPool);
        bullet.SetUpBullet(BulletSpawnPoint.transform.position, BulletSpawnPoint.rotation, playerInfo, bulletDamage);
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.SetUpBullet(BulletSpawnPoint.transform.position, BulletSpawnPoint.rotation, playerInfo, bulletDamage);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        ElympicsDestroy(bullet.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_bulletPool?.Get();
        }
    }
}