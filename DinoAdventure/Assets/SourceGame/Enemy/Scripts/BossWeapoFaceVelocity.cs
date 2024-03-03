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
        Debug.Log(moveDirection);
        if (moveDirection.x > 0)
        {
            transform.right = moveDirection;
        }
        else
        {
            transform.right = moveDirection * -1;
        }
    }
}
