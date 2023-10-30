using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = int.MaxValue)]
public class ItemScriptableData : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Enum_GM.ItemPlace place;
    [SerializeField] private PlayerWeapon weaponKind;
    [SerializeField] private string spriteName;
    [SerializeField] private string description;

    public string ItemName { get { return itemName; } }
    public Enum_GM.ItemPlace Place { get { return place; } }
    public PlayerWeapon WeaponKind { get { return weaponKind; } }
    public string SpriteName { get { return spriteName; } }
    public string Description { get { return description; } }

}
