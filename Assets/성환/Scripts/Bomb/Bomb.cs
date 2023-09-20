using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    float desTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyBomb();

    }


    public void DestroyBomb()
    {
        desTime += Time.deltaTime;
        if(desTime > 2f)
        {
            gameObject.SetActive(false);
            desTime = 0f;
        }
    } 
}
