using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataScriptableObject", menuName = "Game/EnemyDataScriptableObject")]
public class EnemyDataScriptableObject : ScriptableObject
{
    public List<EnemyAttribute> ListEnemyData = new List<EnemyAttribute>();
    public List<BulletData> ListBulletData = new List<BulletData>();
    public explosionData explosionData;


    public EnemyAttribute getDataEnemyKey(string key)
    {
        return ListEnemyData.Find(item => item.Key == key);
    }
}
