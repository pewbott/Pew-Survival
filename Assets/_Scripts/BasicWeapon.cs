using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicWeapon : MonoBehaviour, IShooter
{
    [SerializeField]
    private int maxCountInMagazine;

    [SerializeField]
    private int currentBulletCount;

    [SerializeField]
    private int summBulletCount;

    [SerializeField]
    private float timeBetweenShoots;

    [SerializeField]
    private float currentTimeBetweenShoots;

    [SerializeField]
    private float reloadingTime;
    private float currentReloadTimer;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private Transform _visualModelTransform;

    public bool isReloading, isShootPossible;



    private void Start()
    {
        currentBulletCount = maxCountInMagazine;
        currentReloadTimer = 0;
        isShootPossible = true;
    }

    private void Update()
    {
        ReloadMagazine();
        ShootPermit();
    }

    public GameObject _bulletPrefab;


    public void ShootBullet(GameObject bulletPrefab)
    {
        if (!isReloading && isShootPossible)
        {          
            currentTimeBetweenShoots = 0;

            bulletPrefab = _bulletPrefab;

            if (currentBulletCount > 0)
            {
                Instantiate(bulletPrefab, _firePoint.position, _visualModelTransform.rotation);
                currentBulletCount--;
            }
            else
            {
                ReloadRequest();
            }

            isShootPossible = false;
        }
    }

    public void ReloadMagazine()
    {
        if (isReloading)
        {
            int a = maxCountInMagazine - currentBulletCount;

            if (a != 0 && summBulletCount != 0)
            {
                currentReloadTimer += Time.deltaTime;

                if (currentReloadTimer >= reloadingTime)
                {
                    if (summBulletCount > a)
                    {
                        summBulletCount -= a;
                        currentBulletCount += a;
                        isReloading = false;
                    }
                    else
                    {
                        a = summBulletCount;
                        summBulletCount = 0;
                        currentBulletCount += a;
                        isReloading = false;
                    }

                    isReloading = false;
                }
            }
            else
                isReloading = false;
          
        }
        else
            currentReloadTimer = 0;
    }

    public void ReloadRequest()
    {
        isReloading = true;
    }

    public void ShootPermit()
    {
        if (!isShootPossible)
        {
            currentTimeBetweenShoots += Time.deltaTime;
            if (currentTimeBetweenShoots >= timeBetweenShoots)
            {
                isShootPossible = true;
            }
        }
    }

   
}
