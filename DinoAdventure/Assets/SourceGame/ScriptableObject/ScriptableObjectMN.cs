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
    public BulletType bulletType;
    public EnemyAttack attack;
}
[System.Serializable]
public class BulletData
{
    public BulletType type;
    public float timeDestroy;
    public BulletObj Obj;
    public bool isCloneBullet;
    public bool isFollow;
    public float SpeedFollow;
    public float TimeLoopFollow;
    public int numBullet;
    public float SpeedNumBullet;
}
[System.Serializable]
public class explosionData
{
    public GameObject explosion;
    public GameObject explosionRed;
    public GameObject explosionYellow;
    public GameObject explosionClassic;
    public GameObject explosionBazoka;
    public GameObject effBuffHP;
    public GameObject EffectCrescent;
    public GameObject LineEffectCrescent;
}


[System.Serializable]
public enum EnemyAttack
{
    None = 0, Attack1rays,
    Attack3rays, Attack360degrees,
    Attack3raysx2, AttackRandom, AttackMiner,
    BossAttachk1, BossAttachk2, BossAttachk3,
    BossAttachk4, BossAttachk5,
    BossAttachk6, BossAttachk7,
}
[System.Serializable]
public enum BulletType
{
    None = 0, Blue, Yellow, Red, Ellipse, Rhombus, ArrowShape, Circle, FootballShape, Crescent, Miner, FakeBullet
}
[System.Serializable]
public class BossAttribute
{
    public string Key;
    public string EnemyName;
    public float TimeResetBullet;
    public float TimeLoop;
    public float MoveSpeed;
    public int Hp;
    public float TimeRun;
    public EnemyAttack BossAttack;
    public List<BossAbility> listSkill;
}

[System.Serializable]
public class BossAbility
{
    public string name;
    public int Healing;
    public int NumBullet;
    public float SpeedBullet;
    public float timeAbility;
    public float SpeedRotation;
    public BulletType bulletType;
}

public enum WeaponType
{
    None = 0,
    Pistol,
    Bazooka,
}