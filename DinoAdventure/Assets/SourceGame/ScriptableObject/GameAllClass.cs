using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAllClass : MonoBehaviour
{
    [System.Serializable]
    public class EnemyAttribute
    {   
        public string EnemyName;
        public float TimeResetBullet;
        public float SpeedBullet;
        public float MoveSpeed;
        public int Hp;
        public int IndexBullet;
    }
    [System.Serializable]
    public class BulletData
    {
        public GameObject enemyBullet;
    }
    [System.Serializable]
    public class explosionData
    {
        public GameObject explosion;
        public GameObject explosionClassic;
        public GameObject explosionBazoka;
    }
}
