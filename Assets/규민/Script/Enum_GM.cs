using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enum_GM:MonoBehaviour
{
    //»Ò±Õµµ
    public enum Rarity
    {
        legendary,
        unique,
        rare,
        normal
    }

    //¿Â¬¯∫Œ¿ß
    public enum ItemPlace
    {
        weapon,
        clothes,
        shoes,
        earring,
        ring,
        pet
    }

    //¡§∑ƒ ±‚¡ÿ
    public enum SortBy
    {
        name,
        rare
    }

    public enum abilityName
    {
        damage,
        range,
        attackSpeed,
        speed
    }
}
