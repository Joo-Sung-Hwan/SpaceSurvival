using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_2 : Weapon
{
    public float Speed { get; set; }
    public int AttackAbility { get; set; }
    protected bool dir;

    public override void Initalize()
    {
        weaponData.compareNum = 1;
        weaponData.Damage = 10f;
    }
    public override void Attack()
    {
        base.Attack();
        // �÷��̾� �ڽ����� ���� ������Ʈ ������ �����θ� �������ϱ�
        if (dir)
        {
            transform.position += Vector3.left * Time.deltaTime * Speed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * Speed;
        }
    }
}
