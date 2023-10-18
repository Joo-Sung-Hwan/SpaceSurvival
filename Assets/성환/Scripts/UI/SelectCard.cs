using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Grade
{
    Normal,
    Rare,
    Unique
}
public abstract class SelectCard : MonoBehaviour
{
    protected Grade gr;
    public abstract void init();
}
