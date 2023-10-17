using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Barrier : MonoBehaviour
{
    public List<Sprite> barrier = new();
    void Start()
    {
        transform.GetComponent<SpriteAnimation>().SetSprite(barrier, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
