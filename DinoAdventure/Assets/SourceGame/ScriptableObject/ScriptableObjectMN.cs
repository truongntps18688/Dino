using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectMN : Singleton<ScriptableObjectMN>
{
    public EnemyDataScriptableObject EnemyData;

    void Start()
    {
        Debug.Log("load data enemy: " + EnemyData.ListEnemyData.Count);
    }

}


[System.Serializable]
public class EnemyAttribute
{
    public string Key;
    public string EnemyName;
    public float TimeResetBullet;
    public float SpeedBullet;
    public float MoveSpeed;
    public int SpeedRotation;
    public int Hp;
    public int IndexBullet;
    public EnemyAttack attack;
}
[System.Serializable]
public class BulletData
{
    public Sprite sprite;
    public float speed;
}
[System.Serializable]
public class explosionData
{
    public GameObject explosion;
    public GameObject explosionClassic;
    public GameObject explosionBazoka;
}
[System.Serializable]
public enum EnemyAttack
{
    None = 0,
    Attack1rays = 1,
    Attack3rays = 2,
    Attack360degrees = 3,
    Attack3raysx2 = 4,
    AttackRandom = 5,
    AttackMiner = 6,
}