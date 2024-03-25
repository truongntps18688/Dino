using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : MonoBehaviour, IWeapon
{
    void Start()
    {
        PlayerMovement.shootInput += Shoot;
    }



    public virtual void Shoot() => Debug.Log("SHooting!!");
}
