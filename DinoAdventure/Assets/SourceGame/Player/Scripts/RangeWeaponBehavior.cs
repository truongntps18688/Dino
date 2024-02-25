using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponBehavior : MonoBehaviour
{
    private float coolDown = 0;
    [SerializeField] private float coolDownLap;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    private ProjectileBehavior projectileBehavior;

    void Start()
    {
        this.projectileBehavior =  projectilePrefab.GetComponent<ProjectileBehavior>();
        coolDown = coolDownLap;
    }

    void Update()
    {
        coolDown -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && coolDown <= 0f && Time.timeScale != 0)
        {

            DoShoot();
            coolDown = coolDownLap;

        }
    }

    void DoShoot()
    {
        //Shaking
        //Muzzle'
        //Create projectile
        projectileBehavior.CreateProjectile(this.firePoint);
        //Recoil

    }
}
