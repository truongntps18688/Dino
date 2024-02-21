using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : EnemyTrigger
{
    public AILerp aILerp;
    public string key;
    public EnemyAttribute data;
    bool die = false;

    void Start()
    {
        data = ScriptableObjectManager.Instance.EnemyDataScriptableObject.getDataEnemyKey(key);
        if (data == null)
            Destroy(gameObject);

        aILerp.speed = data.MoveSpeed;
        HP = data.Hp;   
    }

    void Update()
    {
        attackEnemy();
        if (HP <= 0 & die == false)
        {
            aILerp.speed = 0;
            animator.SetBool("Die", true);
        }
    }

    void attackEnemy()
    {
        switch (data.attack)
        {
            case EnemyAttack.None:

                break;
            case EnemyAttack.Attack1rays:

                break;
            case EnemyAttack.Attack3rays:

                break;
            case EnemyAttack.Attack360degrees:

                break;
            default:
                break;
        }
    }
}
