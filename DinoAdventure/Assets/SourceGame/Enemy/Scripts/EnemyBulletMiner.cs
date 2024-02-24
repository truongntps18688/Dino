using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMiner : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GameSC.Instance.objPlayer.tag)
        {
            Destroy(gameObject);
        }
    }
}
