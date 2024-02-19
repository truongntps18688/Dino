using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameAllClass;

[CreateAssetMenu(fileName = "EnemyDataScriptableObject", menuName = "Game/EnemyDataScriptableObject")]
public class EnemyDataScriptableObject : ScriptableObject
{
    public List<EnemyAttribute> ListEnemyData = new List<EnemyAttribute>();
    public List<BulletData> ListBulletData = new List<BulletData>();
    public explosionData explosionData;
}
