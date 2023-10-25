using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = int.MaxValue)]
public class ItemScriptableData : ScriptableObject
{
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField] private Enum_GM.ItemPlace place;
    public Enum_GM.ItemPlace Place { get { return place; } }

    [SerializeField] private string spriteName;
    public string SpriteName { get { return spriteName; } }

    [SerializeField] private string description;
    public string Description { get { return description; } }
}
