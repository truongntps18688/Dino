using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : MonoBehaviour
{
    [SerializeField] public string key;
    [SerializeField] public EnemyAttribute data;
    [SerializeField] public AILerp AILerpPath;
    [SerializeField] public Animator animator;
    [SerializeField] public Rigidbody2D rigidbody2D;
    float HP = 0;
    private explosionData explosionData;
    Vector2 direction;
    bool die = false;
    float repeat, timeLoop;
    float repeatLoop;
    BulletData bulletData;

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
        bulletData = ScriptableObjectMN.Instance.EnemyData.getBulletObj(data.bulletType);
        if (data == null)
        {
            Destroy(gameObject);
            Debug.Log("not data enemy key: " + key);
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
            BulletObj clone = Instantiate(bulletData.Obj, transform.position, Quaternion.identity);
            Debug.Log(bulletData.type);
            clone.setData(bulletData);
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

            BulletObj bullet = Instantiate(bulletData.Obj, transform.position, Quaternion.identity);
            bullet.setData(bulletData);
            bullet.Settings(data.SpeedBullet, xBullet, yBullet);
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
            BulletObj bullet = Instantiate(bulletData.Obj, transform.position, Quaternion.identity);
            bullet.setData(bulletData);
            bullet.Settings(data.SpeedBullet, xBullet, yBullet);
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
                int isRight = direction.x > 0 ? 1 : -1; // check huong ban cua vien dan
                BulletObj bullet1 = Instantiate(bulletData.Obj, transform.position, Quaternion.identity);
                bullet1.setData(bulletData);
                bullet1.Settings(data.SpeedBullet, 2 * isRight, 0, new Vector2(2 * isRight, 0));

                BulletObj bullet2 = Instantiate(bulletData.Obj, transform.position, Quaternion.identity);
                bullet2.setData(bulletData);
                bullet2.Settings(data.SpeedBullet, 1 * isRight, 1, new Vector2(1 * isRight, 1));

                BulletObj bullet3 = Instantiate(bulletData.Obj, transform.position, Quaternion.identity);
                bullet3.setData(bulletData);
                bullet3.Settings(data.SpeedBullet, 1 * isRight, -1, new Vector2(1 * isRight, -1));
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
                BulletObj Bullet = Instantiate(bulletData.Obj, transform.position, Quaternion.identity);
                Bullet.setData(bulletData);
                Vector2 moveDirection = (GameSC.Instance.objPlayer.transform.position - transform.position).normalized;
                Bullet.Settings(data.SpeedBullet, moveDirection.x, moveDirection.y, moveDirection);
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
