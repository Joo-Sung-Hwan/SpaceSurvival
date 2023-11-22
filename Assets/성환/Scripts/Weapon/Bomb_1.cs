using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_1 : Weapon
{
    [SerializeField] private CircleCollider2D circle2d;
    [SerializeField] private Animator animator;
    public float BombRange { get; set; }
    public float BombDebuff { get; set; }

    public ParticleSystem particle;
    public Transform sprite;
    public Transform shadow;

    private Vector2 destination;
    private Vector2 startPos;
    private Vector2 dir;

    private int maxBounce = 5;
    private int curBounce;
    private float delay = 0;
    private float gravity = 10f;
    private float curHeight;
    private bool isBomb = false;
    private bool isGrounded = true;

    private Player player;

    public override void Initalize()
    {
        // ù ���� �ʱ�ȭ
        weaponData.Damage = 1f;
        weaponData.compareNum = 2;
        weaponData.destroyTime = 5f;
        player = GameManager.instance.player;
        startPos = player.area.bounds.center;
    }
    public override void Attack()
    {
        // �߻� ����???
        if (!isGrounded)
        {
            curHeight += -gravity * Time.deltaTime;

            // ��ź �̹��� ���Ʒ��� �ٿ��
            sprite.position += new Vector3(0, curHeight, 0) * Time.deltaTime;
            transform.position += (Vector3)dir * Time.deltaTime;
            CheckGroundHit();
        }
    }
    public void CheckGroundHit()
    {
        float maxDis = Vector2.Distance(startPos, destination);
        float dis = Vector3.Distance(transform.position, startPos);

        // �ִ�Ÿ��̰ų� 3�� �ٿ���ϸ� ����
        if (dis < maxDis && curBounce < maxBounce)
        {
            if (sprite.position.y < shadow.position.y)
            {
                sprite.position = shadow.position;
                DirInit(dir / 1.5f);
            }
        }
        else
        {
            Vector3 tmp = shadow.position;
            tmp.y = tmp.y + 0.04f;
            sprite.position = tmp;
            isGrounded = true;
        }
    }
    void DirInit(Vector2 dir)
    {
        // ����, ����, �ٿ ī��Ʈ
        isGrounded = false;
        this.dir = dir;
        curHeight = 1.5f;
        curBounce++;
    }
    public IEnumerator BombParticle(float delay)
    {
        yield return new WaitForSeconds(0.5f);
        ParticleSystem part = Instantiate(particle, transform); // Ǯ�� �Ẹ��.
        AtiveObj(gameObject.transform, false);
        circle2d.enabled = true;
        yield return new WaitForSeconds(delay);
        circle2d.enabled = false;
        Destroy(part.gameObject); 
        gameObject.SetActive(false);
    }
    public void AtiveObj(Transform trans, bool isActive)
    {
        // ��������Ʈ ���ֱ�
        for (int i = 0; i < 2; i++)
        {
            trans.GetChild(i).GetComponent<SpriteRenderer>().enabled = isActive;
        }
    }
    public void ExplosionFinish()
    {
        // �ִϸ��̼ǿ� �־��� �Լ� ������Ʈ ����.
        gameObject.SetActive(false);
    }
}
