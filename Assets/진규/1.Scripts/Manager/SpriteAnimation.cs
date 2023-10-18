using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class SpriteAnimation : MonoBehaviour
{
    List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sr;

    float spriteDelayTime;
    float delayTime = 0;
    int spriteIndex = 0;

    UnityAction action;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprites.Count == 0)
            return;

        delayTime += Time.deltaTime;
        if(delayTime > spriteDelayTime)
        {
            delayTime = 0;
            sr.sprite = sprites[spriteIndex];
            spriteIndex++;
            if(spriteIndex > sprites.Count - 1)
            {
                spriteIndex = 0;
                if (action != null)
                {
                    sprites.Clear();
                    action();
                    action = null;
                }
            }
        }
    }

    void Init()
    {
        delayTime = 0;
        sprites.Clear();
        spriteIndex = 0;
    }

    public void SetSprite(List<Sprite> argSprite, float delayTime)
    {
        Init();
        sprites = argSprite.ToList();
        spriteDelayTime = delayTime;
    }

    public void SetSPrite(List<Sprite> argSprite, float delayTime, UnityAction action)
    {
        Init();
        this.action = action;
        sprites = argSprite.ToList();
        spriteDelayTime = delayTime;
    }
}
