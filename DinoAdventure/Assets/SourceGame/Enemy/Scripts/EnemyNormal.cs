using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : MonoBehaviour
{
    public string key;
    public EnemyAttribute data;
    public AILerp AILerpPath;
    public float HP = 0;
    public Animator animator;
    public Rigidbody2D rigidbody2D;

    private explosionData explosionData;
    Vector2 direction;
    bool die = false;
    float repeat, timeLoop;
    float repeatLoop;
    EnemyBullet enemyBullet;

    //
    float x, y, angel = 0, radius = 10;
    float xBullet, yBullet;
    
    void Start()
    {
        getData();
    }
    void getData()
    {
        if (key.Length == 0)
            key = gameObject.name;
        data = ScriptableObjectMN.Instance.EnemyData.getDataEnemyKey(key);
        explosionData = ScriptableObjectMN.Instance.EnemyData.explosionData;
        enemyBullet = ScriptableObjectMN.Instance.EnemyData.EnemyBulletObj;
        if (data == null)
        {
            Destroy(gameObject);
            return;
        }

        HP = data.Hp;

        repeatLoop = repeat = 1;
        timeLoop = data.TimeResetBullet;

        if (checkAIPath())
            AILerpPath.speed = data.MoveSpeed;
    }

    void Update()
    {
        attackEnemy();
        faceVelocity();
        isDie();
        isMoveNotAILerp();
    }
    void isMoveNotAILerp()
    {
        if (checkAIPath()) return;
        if (rigidbody2D.velocity.x < data.MoveSpeed - 0.2f & rigidbody2D.velocity.x > -data.MoveSpeed + 0.2f)
        {
            x = Random.Range(-1f, 1f);
            y = Random.Range(-1f, 1f);
            rigidbody2D.velocity = new Vector2(x * data.MoveSpeed, y * data.MoveSpeed);
        }
    }
    void isDie()
    {
        if (HP <= 0 & die == false)
        {
            if (checkAIPath())
                AILerpPath.speed = 0;
            animator.SetBool("Die", true);
        }
    }
    void faceVelocity()
    {
        if (checkAIPath())
            direction = AILerpPath.velocity;
        else
            direction = rigidbody2D.velocity;

        transform.localScale = direction.x > 0 ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
    }
    void attackEnemy()
    {
        switch (data.attack)
        {
            case EnemyAttack.None:

                break;
            case EnemyAttack.Attack1rays:
                Attack1rays();
                break;
            case EnemyAttack.Attack3rays:
                Attack3rays();
                break;
            case EnemyAttack.Attack360degrees:
                Attack360degrees();
                break;
            case EnemyAttack.Attack3raysx2:
                Attack3raysx2();
                break;
            case EnemyAttack.AttackRandom:
                AttackRandom();
                break;
            case EnemyAttack.AttackMiner:
                AttackMiner();
                break;
            default:
                break;
        }
    }
    void AttackMiner()
    {
        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0)
        {
            Instantiate(ScriptableObjectMN.Instance.EnemyData.EnemyBulletMiner, transform.position, Quaternion.identity);
            timeLoop = data.TimeResetBullet;
        }
    }
    void AttackRandom()
    {
        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0)
        {
            float xBullet = Random.Range(-0.1f, 0.1f);
            float yBullet = Random.Range(-0.1f, 0.1f);

            EnemyBullet bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            bullet.rigidbody2D.velocity = new Vector2(xBullet * data.SpeedBullet, yBullet * data.SpeedBullet);
            bullet.transform.right = bullet.rigidbody2D.velocity;
            timeLoop = data.TimeResetBullet;
        }
    }
    void Attack3raysx2()
    {
        repeat = 2;
        Attack3rays();
    }
    void Attack360degrees()
    {
        angel = angel + Time.deltaTime * data.SpeedRotation;
        x = transform.position.x + Mathf.Cos(angel) * radius;
        y = transform.position.y + Mathf.Sin(angel) * radius;
        if (angel >= 360)
        {
            angel = 0;
        }
        timeLoop -= Time.deltaTime;
        if (timeLoop < 0 & die == false)
        {
            xBullet = x - transform.position.x;
            yBullet = y - transform.position.y;
            EnemyBullet bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            bullet.rigidbody2D.velocity = new Vector2(xBullet * data.SpeedBullet, yBullet * data.SpeedBullet);
            bullet.transform.right = bullet.rigidbody2D.velocity;
            timeLoop = data.TimeResetBullet;
        }
    }
    void Attack3rays()
    {
        if (!checkAIPath()) return;

        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0)
        {
            if (repeatLoop > 0 & die == false)
            {
                repeatLoop--;
                Vector2 direction = AILerpPath.velocity;
                if (direction.x > 0)
                {
                    EnemyBullet bullet1 = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    bullet1.rigidbody2D.velocity = new Vector2(2 * data.SpeedBullet, 0 * data.SpeedBullet);
                    bullet1.transform.right = new Vector2(2, 0);


                    EnemyBullet bullet2 = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    bullet2.rigidbody2D.velocity = new Vector2(1 * data.SpeedBullet, 1 * data.SpeedBullet);
                    bullet2.transform.right = new Vector2(1, 1);


                    EnemyBullet bullet3 = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    bullet3.rigidbody2D.velocity = new Vector2(1 * data.SpeedBullet, -1 * data.SpeedBullet);
                    bullet3.transform.right = new Vector2(1, -1);

                }
                else
                {
                    EnemyBullet bullet1 = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    bullet1.rigidbody2D.velocity = new Vector2(-2 * data.SpeedBullet, 0 * data.SpeedBullet);
                    bullet1.transform.right = new Vector2(-2, 0);


                    EnemyBullet bullet2 = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    bullet2.rigidbody2D.velocity = new Vector2(-1 * data.SpeedBullet, 1 * data.SpeedBullet);
                    bullet2.transform.right = new Vector2(-1, 1);


                    EnemyBullet bullet3 = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    bullet3.rigidbody2D.velocity = new Vector2(-1 * data.SpeedBullet, -1 * data.SpeedBullet);
                    bullet3.transform.right = new Vector2(-1, -1);

                }
                timeLoop = 0.3f;

            }
            else
            {
                timeLoop = data.TimeResetBullet;
                repeatLoop = repeat;
            }

        }
    }
    void Attack1rays()
    {
        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0)
        {
            if (repeatLoop > 0)
            {
                repeatLoop--;
                EnemyBullet EnemyBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                EnemyBullet.setData(ScriptableObjectMN.Instance.EnemyData.getBulletObj(data.IndexBullet));
                Vector2 moveDirection = (GameSC.Instance.objPlayer.transform.position - transform.position).normalized;
                EnemyBullet.rigidbody2D.velocity = new Vector2(moveDirection.x * data.SpeedBullet, moveDirection.y * data.SpeedBullet);
                EnemyBullet.transform.right = new Vector2(moveDirection.x, moveDirection.y);

                timeLoop = 0.2f;
            }
            else
            {
                timeLoop = data.TimeResetBullet;
                repeatLoop = repeat;
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
    }
    public bool checkAIPath()
    {
        return AILerpPath != null;
    }
}
