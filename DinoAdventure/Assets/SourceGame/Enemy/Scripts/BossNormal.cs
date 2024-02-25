using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormal : MonoBehaviour
{
    [SerializeField] public string key;
    [SerializeField] public BossAttribute data;
    float HP = 0, TimeRun = 0, SpeedBullet = 0, timeRe = 0;
    bool isRun = false, die = false, isAttack = false;
    public AILerp AILerpPath;
    public Animator animator;
    public Transform pointBullet;
    float timeLoop;
    void Start()
    {
        getData();
    }
    void getData()
    {
        if (key.Length == 0)
            key = gameObject.name;
        data = ScriptableObjectMN.Instance.EnemyData.getDataBossKey(key);
        if(data == null)
        {
            Destroy(gameObject);
            Debug.Log("not data boss key: " + key);
            return;
        }
        HP = data.Hp;
        TimeRun = data.TimeRun;
        SpeedBullet = data.SpeedBullet;
        timeRe = timeLoop = data.TimeLoop;
        AILerpPath.speed = 0;
        if (SpeedBullet == 0)
            Debug.Log("bullet da co van toc rieng trong prefab");

        // neu data.speedbuleet = 0 thì bullet da co speed rieng
    }

    void Update()
    {
        TimeRunBoss();
        if (HP <= 0 & die == false)
        {
            AILerpPath.speed = 0;
            animator.SetBool("Die", true);
            die = true;
        }
        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0 & isAttack == false & die == false)
        {
            isAttack = true;
            attack();
        }
    }
    void TimeRunBoss()
    {
        TimeRun -= Time.deltaTime;
        if (TimeRun <= 0 & isRun == false)
        {
            isRun = true;
            AILerpPath.speed = data.MoveSpeed;
        }
    }
    void attack()
    {
        switch (data.BossAttack)
        {
            case EnemyAttack.None:
                break;
            case EnemyAttack.BossAttachk1:
                BossAttachk1();
                break;
            case EnemyAttack.BossAttachk2:
                BossAttachk2();
                break;
            case EnemyAttack.BossAttachk3:
                BossAttachk3();
                break;
            case EnemyAttack.BossAttachk4:
                BossAttachk4();
                break;
            case EnemyAttack.BossAttachk5:
                BossAttachk5();
                break;
            default:
                break;
        }
    }
    void BossAttachk1()
    {

    }
    void BossAttachk2()
    {

    }
    void BossAttachk3()
    {

    }
    void BossAttachk4()
    {

    }
    void BossAttachk5()
    {

    }
}
