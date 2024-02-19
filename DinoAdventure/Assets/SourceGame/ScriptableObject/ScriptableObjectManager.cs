using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectManager : Singleton<ScriptableObjectManager>
{
    public EnemyDataScriptableObject EnemyDataScriptableObject;

    void Start()
    {
        Debug.Log("load data enemy: " + EnemyDataScriptableObject.ListEnemyData.Count);
    }

}
