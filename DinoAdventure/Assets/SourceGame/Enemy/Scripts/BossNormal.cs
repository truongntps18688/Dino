using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormal : MonoBehaviour
{
    [SerializeField] public string key;
    [SerializeField] public BossAttribute data;
    float HP = 0, TimeRun = 0, timeRe = 0, rdX, rdY, timeLoop, angel = 0, x, y;
    float xBullet, yBullet;
    bool isRun = false, die = false, isAttack = false;
    public AILerp AILerpPath;
    public Animator animator;
    public Transform pointBullet, pointBullet1, pointBullet2, pointBullet3;
    public GameObject weapon1, weapon2, weapon3;
    public GameObject cageObj, effCageObj1, effCageObj2;
    List<int> listNumbullet = new List<int>();
    List<int> listNumbulletCurr = new List<int>();
    int indexAttack = 0;
    bool checkHP = false;
    private explosionData explosionData;
    public EnemyNormal enemySkull;
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
        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0 & indexAttack == 0 & die == false)
        {
            int rd = HP < data.Hp ? Random.Range(1, 5) : Random.Range(1, 4); // ko su hoi mau khi mau dang day
            if (rd == 4 && checkHP == true)
            {
                rd = Random.Range(1, 4);
            }
            if (rd == 1)
            {
                indexAttack = 1;
            }
            else if (rd == 2)
            {
                indexAttack = 2;
                listNumbullet[1] = listNumbulletCurr[1];
            }
            else if (rd == 3)
            {
                indexAttack = 3;
                listNumbullet[2] = listNumbulletCurr[2];
            }
            else if (rd == 4 && checkHP == false)
            {
                indexAttack = 4;
                GameObject eff = Instantiate(explosionData.effBuffHP, transform.position, Quaternion.identity);
                Destroy(eff, 2.5f);
                listNumbullet[3] = listNumbulletCurr[3];
                timeLoop = data.listSkill[3].timeAbility;
                checkHP = true;
            }
        }
        if (indexAttack == 1)
            BossAttachk2_1();
        if (indexAttack == 2)
            BossAttachk2_2();
        if (indexAttack == 3)
            BossAttachk2_3();
        if (indexAttack == 4)
            BossAttachk2_4();
    }
    void BossAttachk3()
    {
        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0 & indexAttack == 0 & die == false)
        {
            AILerpPath.speed = 0f;
            int rd = Random.Range(1, 4);
            indexAttack = rd = 1;
            setActiveWeapons(indexAttack);
        }
        BossAttachk3_1();
        BossAttachk3_2();
        BossAttachk3_3();


        // working
    }
    void BossAttachk4()
    {

    }
    void BossAttachk5()
    {

    }
    void BossAttachk3_1()
    {
        if (timeLoop <= 0 & indexAttack == 1 & die == false)
        {
            listNumbullet[0]--;
            Vector2 moveDirection = (GameSC.Instance.objPlayer.transform.position - transform.position).normalized;

            Instantiate(explosionData.explosionRed, pointBullet.position, Quaternion.identity);
            BulletData bulletData = ScriptableObjectMN.Instance.EnemyData.getBulletObj(data.listSkill[0].bulletType);
            BulletObj bullet = Instantiate(bulletData.Obj, pointBullet.position, Quaternion.identity);
            bullet.setData(bulletData);
            bullet.Settings(data.listSkill[0].SpeedBullet, moveDirection.x, moveDirection.y);

            timeLoop = data.listSkill[0].timeAbility;
            if (listNumbullet[0] <= 0)
            {
                AILerpPath.speed = data.MoveSpeed;
                timeLoop = timeRe;
                indexAttack = 0;
                listNumbullet[0] = listNumbulletCurr[0];
            }
        }
    }
    void BossAttachk3_2()
    {

    }
    void BossAttachk3_3()
    {

    }
    void BossAttachk2_1()
    {
        Instantiate(enemySkull, pointBullet.position, Quaternion.identity);
        Instantiate(enemySkull, pointBullet1.position, Quaternion.identity);
        Instantiate(enemySkull, pointBullet2.position, Quaternion.identity);
        Instantiate(enemySkull, pointBullet3.position, Quaternion.identity);
        timeLoop = timeRe;
        indexAttack = 0;
    }
    void BossAttachk2_2()
    {
        angel = angel + Time.deltaTime * data.listSkill[1].SpeedRotation;
        x = transform.position.x + Mathf.Cos(angel) * 10;
        y = transform.position.y + Mathf.Sin(angel) * 10;
        if (angel >= 360)
        {
            angel = 0;
        }
        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0)
        {
            listNumbullet[1]--;

            xBullet = x - transform.position.x;
            yBullet = y - transform.position.y;
            BulletData bulletData = ScriptableObjectMN.Instance.EnemyData.getBulletObj(data.listSkill[1].bulletType);
            BulletObj bullet = Instantiate(bulletData.Obj, transform.position, Quaternion.identity);
            bullet.setData(bulletData);
            bullet.Settings(data.listSkill[1].SpeedBullet, xBullet, yBullet);

            BulletObj bullet1 = Instantiate(bulletData.Obj, transform.position, Quaternion.identity);
            bullet1.setData(bulletData);
            bullet1.Settings(-data.listSkill[1].SpeedBullet, xBullet, yBullet);

            timeLoop = data.listSkill[1].timeAbility;
        }
        if (listNumbullet[1] <= 0)
        {
            indexAttack = 0;
            timeLoop = timeRe;
        }
    }
    void BossAttachk2_3()
    {
        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0)
        {
            listNumbullet[2]--;
            BulletData bulletData = ScriptableObjectMN.Instance.EnemyData.getBulletObj(data.listSkill[2].bulletType);

            BulletObj bullet = Instantiate(bulletData.Obj, pointBullet.position, Quaternion.identity);
            bullet.setData(bulletData);
            bullet.Settings(1, 0, 2);
            BulletObj bullet1 = Instantiate(bulletData.Obj, pointBullet.position, Quaternion.identity);
            bullet1.setData(bulletData);
            bullet1.Settings(1, 0, -2);
            BulletObj bullet2 = Instantiate(bulletData.Obj, pointBullet.position, Quaternion.identity);
            bullet2.setData(bulletData);
            bullet2.Settings(1, 2, 0);
            BulletObj bullet3 = Instantiate(bulletData.Obj, pointBullet.position, Quaternion.identity);
            bullet3.setData(bulletData);
            bullet3.Settings(1, -2, 0);
            timeLoop = data.listSkill[2].timeAbility;
        }
        if (listNumbullet[2] <= 0)
        {
            indexAttack = 0;
            timeLoop = timeRe;
        }
    }
    void BossAttachk2_4()
    {
        AILerpPath.speed = 0;
        timeLoop -= Time.deltaTime;

        if (timeLoop <= 0)
        {
            listNumbullet[3]--;
            HP += 10;
            timeLoop = data.listSkill[3].timeAbility;
            if (HP > data.Hp)
            {
                HP = data.Hp;
            }
        }
        if (listNumbullet[3] <= 0)
        {
            AILerpPath.speed = data.MoveSpeed;
            indexAttack = 0;
            timeLoop = timeRe;
        }
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
    void setActiveWeapons(int attack)
    {
        weapon1.SetActive(attack == 1);
        weapon2.SetActive(attack == 2);
        weapon3.SetActive(attack == 3);
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

        if (HP <= (data.Hp / 2) & isRun == false && cageObj != null)
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