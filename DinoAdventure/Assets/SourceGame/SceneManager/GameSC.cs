using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSC : Singleton<GameSC>
{
    public GameObject objPlayer;
    public void setObjPlayer(GameObject obj)
    {
        objPlayer = obj;
    }
    void Start()
    {
        
    }

    

}
