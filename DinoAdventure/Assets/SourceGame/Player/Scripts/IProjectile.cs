using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    float Speed { get; set; }
    float Accuracy { get; set; }

    void Spawn();

}
