using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Bullet
{
    
    public override void init()
    {
        Speed = 2f;
    }
    public override void SetDir()
    {
        base.SetDir();
    }

    

    void Start()
    {
        init();
    }

    private void OnEnable()
    {
        SetDir();
    }
    private void Update()
    {
        
    }
    
}
