using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapoFaceVelocity : MonoBehaviour
{
    Vector2 moveDirection;
    Vector3 transformStart;

    void Start()
    {
        transformStart = transform.localScale;
    }

    void Update()
    {
        faceVelocity();
    }

    void faceVelocity()
    {
        moveDirection = (GameSC.Instance.objPlayer.transform.position - transform.position).normalized;
        transform.right = moveDirection.x > 0 ? moveDirection : moveDirection * -1;
    }
}
