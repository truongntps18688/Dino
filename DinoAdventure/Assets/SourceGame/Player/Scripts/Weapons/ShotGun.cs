using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShotGun : Weapon
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float _speed;

    public override void Shoot()
    {
        //for (int i = 0; i < 5; i++)
        //{
        //    var rd = Random.Range(1, 8);
        //    var obj = ObjectPool.SharedInstance.CreateProjectile(firePoint);
        //    obj.transform.rotation = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y + rd, firePoint.rotation.z);
        //    obj.GetComponent<Rigidbody2D>().velocity = firePoint.right * _speed;

        //}

    }
}
