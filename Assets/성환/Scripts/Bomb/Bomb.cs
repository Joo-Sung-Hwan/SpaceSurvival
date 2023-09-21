using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    float desTime = 0f;
    Vector3 start_pos;
    float dis;
    Vector2 destination;
    float maxdis;
    float gravity = 10f;

    Vector2 dir;
    
    bool isGrounded = true;

    float maxheight;
    float curheight;

    public Transform sprite;
    public Transform shadow;

    Player player;
    // Start is called before the first frame update

    
    void OnEnable()
    {
        player = GameManager.instance.playerSpawnManager.player;
        start_pos = player.area.bounds.center;
        destination = player.GetRandomPosition();
        maxdis = Vector3.Distance(player.area.bounds.center, destination);
        Debug.Log(player.area.bounds.center);
        curheight = 1f;
        maxheight = curheight;
        Init(destination);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyBomb();
        if (!isGrounded)
        {
            curheight += -gravity * Time.deltaTime;
            sprite.position += new Vector3(0, curheight, 0) * Time.deltaTime;
            transform.position += (Vector3)dir * Time.deltaTime;

            CheckGroundHit();
        }
    }


    public void DestroyBomb()
    {
        desTime += Time.deltaTime;
        if(desTime > 3f)
        {
            gameObject.SetActive(false);
            desTime = 0f;
        }
    } 
    public void Init(Vector2 dir)
    {
        isGrounded = false;
        maxheight = 1.5f;
        this.dir = dir / 1.2f;
        curheight = maxheight;
    }

    public void CheckGroundHit()
    {
        dis = Vector3.Distance(this.transform.position, start_pos);
        if(dis < maxdis)
        {
            if (sprite.position.y < shadow.position.y)
            {
                sprite.position = shadow.position;
                Init(dir);
            }
            
        }
        else
        {
            //Debug.Log("½ºÅ¾");
            sprite.position = shadow.position;
            isGrounded = true;
        }
        /*
        if(sprite.position.y < shadow.position.y)
        {
            sprite.position = shadow.position;
            if(curBounce < maxBounce)
            {
                Init(dir);
            }
            else
            {
                isGrounded = true;
            }
        }
        */
    }
}
