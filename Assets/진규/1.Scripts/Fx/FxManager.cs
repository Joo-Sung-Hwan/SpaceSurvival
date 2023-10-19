using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FxData
{
    public float Attack { get; set; }
}
public abstract class FxManager : MonoBehaviour
{
    public FxData fd = new();

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Init();
}
