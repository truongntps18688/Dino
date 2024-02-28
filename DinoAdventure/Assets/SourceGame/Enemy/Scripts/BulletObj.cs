using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rigidbody2D;
    public BulletData data;
    BulletObj objClone;
    public void setData(BulletData _data)
    {
        data = _data;
        Destroy(gameObject, data.timeDestroy);
        if (data.isCloneBullet)
        {
            objClone = ScriptableObjectMN.Instance.EnemyData.getBulletObj(BulletType.Blue).Obj;
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
        if (data.isCloneBullet)
        {
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
    }
}
