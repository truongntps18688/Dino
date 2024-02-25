using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataScriptableObject", menuName = "Game/PlayerDataScriptableObject")]
public class PlayerDataScriptableObject : ScriptableObject
{
    public PlayerAttribute playerAttribute;
    public List<WeaponData> listWeapon = new List<WeaponData>();
    public List<BulletWeaponData> listBulletWeapon = new List<BulletWeaponData>();
    public List<SkinData> listSkin = new List<SkinData>();
}
