using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float _speed;

    public override void Shoot()
    {
        var obj = ObjectPool.SharedInstance.CreateProjectile(firePoint);
    }
}
