using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataScriptableObject", menuName = "Game/EnemyDataScriptableObject")]
public class EnemyDataScriptableObject : ScriptableObject
{
    public List<EnemyAttribute> ListEnemyData = new List<EnemyAttribute>();
    public List<BossAttribute> listBossdata = new List<BossAttribute>();
    public List<BulletData> ListBullet = new List<BulletData>();
    public explosionData explosionData;


    public EnemyAttribute getDataEnemyKey(string key)
    {
        return ListEnemyData.Find(item => item.Key == key);
    }
    public BossAttribute getDataBossKey(string key)
    {
        return listBossdata.Find(item => item.Key == key);
    }
    public BulletData getBulletObj(BulletType type)
    {
        return ListBullet.Find(item => item.type == type);
    }
}
