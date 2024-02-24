using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.5f);
    }
    public void setData(Sprite img = null)
    {
        sprite.sprite = img;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Instantiate(ScriptableObjectManager.Instance.EnemyDataScriptableObject.explosionData.explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
