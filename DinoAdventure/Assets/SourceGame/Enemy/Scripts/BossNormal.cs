using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormal : MonoBehaviour
{
    [SerializeField] public string key;
    [SerializeField] public BossAttribute data;
    float HP = 0, TimeRun = 0, timeRe = 0, rdX, rdY, timeLoop;
    bool isRun = false, die = false, isAttack = false;
    public AILerp AILerpPath;
    public Animator animator;
    public Transform pointBullet;
    public GameObject cageObj, effCageObj1, effCageObj2;
    List<int> listNumbullet = new List<int>();
    List<int> listNumbulletCurr = new List<int>();

    private explosionData explosionData;

    void Start()
    {
        getData();
    }
    void getData()
    {
        if (key.Length == 0)
            key = gameObject.name;
        data = ScriptableObjectMN.Instance.EnemyData.getDataBossKey(key);
        explosionData = ScriptableObjectMN.Instance.EnemyData.explosionData;
        if (data == null)
        {
            Destroy(gameObject);
            Debug.Log("not data boss key: " + key);
            return;
        }
        HP = data.Hp;
        TimeRun = data.TimeRun;
        timeRe = timeLoop = data.TimeLoop;
        AILerpPath.speed = 0;
        for (int i = 0; i < data.listSkill.Count; i++)
        {
            listNumbullet.Add(data.listSkill[i].NumBullet);
            listNumbulletCurr.Add(data.listSkill[i].NumBullet);
        }
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
        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0 && isAttack == false && die == false)
        {
            isAttack = true;
            BossAttachk1_1();
        }
        BossAttachk1_2();
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
    void BossAttachk1_1()
    {
        if (isRun == true)
        {
            listNumbullet[0]--;
            BulletData bulletData = ScriptableObjectMN.Instance.EnemyData.getBulletObj(data.listSkill[0].bulletType);
            BulletObj bullet = Instantiate(bulletData.Obj, transform.position, Quaternion.identity);
            bullet.setData(bulletData);
            Vector2 moveDirection = (GameSC.Instance.objPlayer.transform.position - transform.position).normalized;
            bullet.Settings(data.listSkill[0].SpeedBullet, moveDirection.x, moveDirection.y, moveDirection);
            if(listNumbullet[0] <= 0)
            {
                listNumbullet[0] = listNumbulletCurr[0];
            }
        }

    }
    void BossAttachk1_2()
    {
        if (isAttack == true & timeLoop <= 0)
        {
            listNumbullet[1]--;
            Vector2 moveDirection = (GameSC.Instance.objPlayer.transform.position - transform.position).normalized;
            rdX = moveDirection.x > 0 ? Random.Range(0.001f, moveDirection.x) : Random.Range(moveDirection.x, 0.001f);
            rdY = moveDirection.y < 0 ? Random.Range(0.001f, moveDirection.y) : Random.Range(moveDirection.y, 0.001f);
            BulletData bulletData = ScriptableObjectMN.Instance.EnemyData.getBulletObj(data.listSkill[1].bulletType);
            BulletObj bullet = Instantiate(bulletData.Obj, pointBullet.position, Quaternion.identity);
            bullet.setData(bulletData);
            bullet.Settings(data.listSkill[1].SpeedBullet, moveDirection.x, moveDirection.y);

            timeLoop = data.TimeResetBullet;
            if (listNumbullet[1] <= 0)
            {
                BulletData bulletData1 = ScriptableObjectMN.Instance.EnemyData.getBulletObj(data.listSkill[0].bulletType);
                BulletObj bullet1 = Instantiate(bulletData1.Obj, pointBullet.position, Quaternion.identity);
                bullet1.setData(bulletData1);
                bullet1.Settings(data.listSkill[0].SpeedBullet, moveDirection.x, moveDirection.y);
                timeLoop = data.TimeLoop;
                isAttack = false;
                listNumbullet[1] = listNumbulletCurr[1];
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet_classic")
        {
            HP -= 2;
            animator.SetBool("Hurt", true);
            GameObject effect = Instantiate(explosionData.explosionClassic, transform.position, Quaternion.identity);
            Destroy(effect, 0.25f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "bullet_korth")
        {
            HP -= 6;
            animator.SetBool("Hurt", true);
            GameObject effect = Instantiate(explosionData.explosionClassic, transform.position, Quaternion.identity);
            Destroy(effect, 0.25f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "bullet_sniper")
        {
            HP -= 18;
            animator.SetBool("Hurt", true);
            GameObject effect = Instantiate(explosionData.explosionClassic, transform.position, Quaternion.identity);
            Destroy(effect, 0.25f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "bullet_bazooka" | collision.gameObject.tag == "bullet_miner")
        {
            animator.SetBool("Hurt", true);
            GameObject effect = Instantiate(explosionData.explosionBazoka, transform.position, Quaternion.identity);
            Destroy(effect, 0.25f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "arrowforbow")
        {
            HP -= 18;
            if (HP > 0)
            {
                animator.SetBool("Hurt", true);
            }
        }

        if (collision.gameObject.tag == "eplBazooka")
        {
            HP -= 12;
            animator.SetBool("Hurt", true);
        }
        if (collision.gameObject.tag == "Melee")
        {
            HP -= 8;
            animator.SetBool("Hurt", true);
        }

        if (HP <= (data.Hp / 2) & isRun == false)
        {
            isRun = true;
            AILerpPath.speed = data.MoveSpeed;
            Destroy(cageObj);
            GameObject eff = Instantiate(effCageObj1, transform.position, Quaternion.identity);
            GameObject eff2 = Instantiate(effCageObj2, transform.position, Quaternion.identity);
            Destroy(eff2, 0.25f);
            Destroy(eff, 2f);
        }
    }
}