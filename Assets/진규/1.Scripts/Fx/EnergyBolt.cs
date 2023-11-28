using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : Weapon
{
    public override void Initalize()
    {
        weaponData.Damage = 10;
    }

    /*public override void Attack()
    {
        Player player = GameManager.instance.player;
        if(player.levelup)
        {
            int val = 360 / player.index;
            int temVal = val;
            for (int i = 0; i < player.index; i++)
            {
                player.enegyTrans[i].gameObject.transform.parent.gameObject.SetActive(true);
                player.enegyTrans[i].gameObject.transform.parent.rotation = Quaternion.Euler(new Vector3(0f, 0f, temVal));
                temVal += val;
            }

            player.levelup = false;
            foreach (var trans in player.enegyTrans)
            {
                trans.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            player.enegy.Rotate(Vector3.forward * Time.deltaTime * 70f);
        }
    }*/
    public override void PlayAct(Collider2D collider)
    {
        Enemy e = collider.GetComponent<Enemy>();
        e.Hp -= weaponData.Damage;
        e.CreateDamageTxt(weaponData.Damage);
    }
}
