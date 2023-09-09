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
    [SerializeField] private int ammoPerBullet;

    [SerializeField] private GameObject bulletPrefab;
    
    public void Attack(long tick)
    {
        if (playerInfo.SpendAmmo(ammoPerBullet))
        {
            Bullet bullet = CreateBullet();
            bullet.SetTick(tick);
        }
    } 
    
    private Bullet CreateBullet()
    {

        Bullet bullet = ElympicsInstantiate(bulletPrefab.name, ElympicsPlayer.All).GetComponent<Bullet>();
        bullet.SetUpBullet(BulletSpawnPoint.transform.position, BulletSpawnPoint.rotation, playerInfo, bulletDamage, BulletSpawnPoint.forward);
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.SetUpBullet(BulletSpawnPoint.transform.position, BulletSpawnPoint.rotation, playerInfo, bulletDamage, transform.forward);
    }

}
