using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataScriptableObject", menuName = "Game/EnemyDataScriptableObject")]
public class EnemyDataScriptableObject : ScriptableObject
{
    public List<EnemyAttribute> ListEnemyData = new List<EnemyAttribute>();
    public List<BossAttribute> listBossdata = new List<BossAttribute>();
    public List<Sprite> ListBulletData = new List<Sprite>();
    public explosionData explosionData;
    public EnemyBullet EnemyBulletObj;
    public EnemyBulletMiner EnemyBulletMiner;


    public EnemyAttribute getDataEnemyKey(string key)
    {
        return ListEnemyData.Find(item => item.Key == key);
    }
    public BossAttribute getDataBossKey(string key)
    {
        return listBossdata.Find(item => item.Key == key);
    }
    public Sprite getBulletObj(int index)
    {
        return ListBulletData[index];
    }
}
