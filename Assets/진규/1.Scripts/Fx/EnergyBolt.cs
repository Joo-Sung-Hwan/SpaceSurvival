using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : MonoBehaviour
{
    public List<Sprite> enegyBolt = new();
    public List<Sprite> enegyBolt1 = new();

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SpriteAnimation>().SetSprite(enegyBolt1, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
