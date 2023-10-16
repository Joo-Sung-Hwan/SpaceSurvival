using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PetData
{
    public float speed;
}

public abstract class Pet : MonoBehaviour
{
    public PetData petData = new();
    public Animator anim;
    protected Player player;

    public abstract void Init();

    void Update()
    {
        player = GameManager.instance.playerSpawnManager.player;
        Move();
    }

    void Move()
    {
        Vector3 dir = player.transform.position - transform.position;

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (dir.normalized.x > 0)
            transform.GetComponent<SpriteRenderer>().flipX = true;
        else if(dir.normalized.x < 0)
            transform.GetComponent<SpriteRenderer>().flipX = false;
        if (distance > 0.5f)
        {
            transform.Translate(dir.normalized * Time.deltaTime * petData.speed);
        }
    }
}
