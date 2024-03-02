using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormal : MonoBehaviour
{
    [SerializeField] public string key;
    [SerializeField] public BossAttribute data;
    float HP = 0, TimeRun = 0, timeRe = 0;
    bool isRun = false, die = false, isAttack = false;
    public AILerp AILerpPath;
    public Animator animator;
    public Transform pointBullet;
    public GameObject cageObj, effCageObj1, effCageObj2;
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
        timeRe = timeLoop = data.TimeResetBullet;
        AILerpPath.speed = 0;
    }

    void Update()
    {
        TimeRunBoss();
        if (HP <= 0 && die == false)
        {
            AILerpPath.speed = 0;
            animator.SetBool("Die", true);
            die = true;
        }
        attack();
    }
    void TimeRunBoss()
    {
        TimeRun -= Time.deltaTime;
        if (TimeRun <= 0 && isRun == false)
        {
            isRun = true;
            AILerpPath.speed = data.MoveSpeed;
            if(cageObj != null)
            {
                cageObj.SetActive(false);
                GameObject eff = Instantiate(effCageObj1, transform.position, Quaternion.identity);
                GameObject eff2 = Instantiate(effCageObj2, transform.position, Quaternion.identity);
                Destroy(eff2, 0.25f);
                Destroy(eff, 2f);
            }
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
