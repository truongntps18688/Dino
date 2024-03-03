using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rigidbody2D;
    public BulletData data;
    BulletObj objClone;

    float timeLoop = 0.5f, Speed = 1.5f;
    Vector2 moveDirection;
    bool isFakeBulletCrescent = false;
    public void setData(BulletData _data)
    {
        data = _data;
        Destroy(gameObject, data.timeDestroy);
        if (data.isCloneBullet)
        {
            objClone = ScriptableObjectMN.Instance.EnemyData.getBulletObj(BulletType.Circle).Obj;
        }
        if (data.isFollow)
        {
            timeLoop = data.TimeLoopFollow;
            Speed = data.SpeedFollow;
        }
    }
    private void Update()
    {
        if (data.isFollow)
        {
            timeLoop -= Time.deltaTime;
            if (timeLoop <= 0)
            {
                Vector2 moveDirection = (GameSC.Instance.objPlayer.transform.position - transform.position).normalized;
                rigidbody2D.velocity = new Vector2(moveDirection.x * Speed, moveDirection.y * Speed);
                transform.right = new Vector2(moveDirection.x, moveDirection.y);
                timeLoop = data.TimeLoopFollow * 10;
            }
        }
        if(data.type == BulletType.None || isFakeBulletCrescent)
        {
            FakeBulletCrescent();
        }
    }
    public void FakeBulletCrescent()
    {
        rigidbody2D.velocity = new Vector2(moveDirection.x * Speed, moveDirection.y * Speed);
        transform.right = new Vector2(moveDirection.x, moveDirection.y);
        timeLoop -= Time.deltaTime;
        if (timeLoop <= 0)
        {
            Instantiate(ScriptableObjectMN.Instance.EnemyData.explosionData.LineEffectCrescent, transform.position, Quaternion.identity);
            timeLoop = 0.2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Instantiate(ScriptableObjectMN.Instance.EnemyData.explosionData.explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            checkCloneBullet();
        }
    }
    void checkCloneBullet()
    {
        if (!data.isCloneBullet) return;
        for (int i = 0; i < data.numBullet; i++)
        {
            float rdX = Random.Range(-0.1f, 0.1f);
            float rdY = Random.Range(-0.1f, 0.1f);
            if (objClone == null) return;
            BulletObj b = Instantiate(objClone, transform.position, Quaternion.identity);
            b.rigidbody2D.velocity = new Vector2(rdX * data.SpeedNumBullet, rdY * data.SpeedNumBullet);
            b.transform.right = b.rigidbody2D.velocity;
        }
    }
    public void Settings(float SpeedBullet, float xVelocity, float yVelocity, Vector2 vector)
    {
        rigidbody2D.velocity = new Vector2(xVelocity * SpeedBullet, yVelocity * SpeedBullet);
        transform.right = vector;
    }
    public void Settings(float SpeedBullet, float xVelocity, float yVelocity)
    {
        rigidbody2D.velocity = new Vector2(xVelocity * SpeedBullet, yVelocity * SpeedBullet);
        transform.right = rigidbody2D.velocity;
    }
    public void Settings(float SpeedBullet, Vector2 vector)
    {
        rigidbody2D.AddForce(vector * SpeedBullet, ForceMode2D.Impulse);
        transform.up = vector;
    }
    public void SettingsFakeBulletCrescent(float SpeedBullet, Vector2 vector)
    {
        moveDirection = vector;
        Speed = SpeedBullet;
        isFakeBulletCrescent = true;
        Destroy(gameObject, 5f);
    }
}
