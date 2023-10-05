using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enum_GM:MonoBehaviour
{
    public enum Rarity
    {
        legendary,
        unique,
        rare,
        normal
    }

    public enum ItemPlace
    {
        weapon,
        clothes,
        shoes,
        earring,
        ring,
        pet
    }

    public enum SortBy
    {
        name,
        rare
    }
}
