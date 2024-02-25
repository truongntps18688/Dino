using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectMN : Singleton<ScriptableObjectMN>
{
    public EnemyDataScriptableObject EnemyData;
    public PlayerDataScriptableObject PlayerData;

    void Start()
    {
        Debug.Log("load data enemy: " + EnemyData.ListEnemyData.Count);
    }

}
[System.Serializable]
public class SkinData
{
    public string name;
    public Sprite idle;
    public Animator animator;
}
[System.Serializable]
public class WeaponData
{
    public string name;
    public WeaponType WeaponType;
    public int Dmg;
    public float CoolDown;
    public float Recoil;
    public float WindowShake;
    public int IndexBulletWeaponData;
}
[System.Serializable]
public class BulletWeaponData
{
    public string name;
    public int SpeedBullet;
    public GameObject prefab;
}

[System.Serializable]
public class PlayerAttribute
{
    public string PlayerName;
    public float MoveSpeed;
    public int HightScore;
    public int UnlockWeapon;
    public int indexSkin;
    public int currWeapon;
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
    BossAttachk1 = 7,
    BossAttachk2 = 8,
    BossAttachk3 = 9,
    BossAttachk4 = 10,
    BossAttachk5 = 11,
    BossAttachk6 = 12,
    BossAttachk7 = 13,
}
[System.Serializable]
public class BossAttribute
{
    public string Key;
    public string EnemyName;
    public float MoveSpeed;
    public float SpeedBullet;
    public float TimeLoop;
    public int Hp;
    public float TimeRun;
    public EnemyAttack BossAttack;
}


public enum WeaponType
{
    None = 0,
    Pistol = 1,
    Bazooka = 2,
}