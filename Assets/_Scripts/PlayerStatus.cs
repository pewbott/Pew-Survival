using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : BasicUnitInfo
    
{
    [SerializeField]
    BasicWeapon _playerWeapon;
    
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            _playerWeapon.ShootBullet(_playerWeapon._bulletPrefab);
       
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            _playerWeapon.ReloadRequest();
        }
    }
}
