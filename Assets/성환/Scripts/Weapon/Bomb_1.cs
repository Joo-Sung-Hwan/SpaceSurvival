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
        // 첫 스팩 초기화
        weaponData.Damage = 1f;
        weaponData.compareNum = 2;
        weaponData.destroyTime = 5f;
        player = GameManager.instance.player;
        startPos = player.area.bounds.center;
    }
    public override void Attack()
    {
        // 발사 시작???
        if (!isGrounded)
        {
            curHeight += -gravity * Time.deltaTime;

            // 폭탄 이미지 위아래로 바운드
            sprite.position += new Vector3(0, curHeight, 0) * Time.deltaTime;
            transform.position += (Vector3)dir * Time.deltaTime;
            CheckGroundHit();
        }
    }
    public void CheckGroundHit()
    {
        float maxDis = Vector2.Distance(startPos, destination);
        float dis = Vector3.Distance(transform.position, startPos);

        // 최대거리이거나 3번 바운드하면 터짐
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
        // 방향, 높이, 바운스 카운트
        isGrounded = false;
        this.dir = dir;
        curHeight = 1.5f;
        curBounce++;
    }
    public IEnumerator BombParticle(float delay)
    {
        yield return new WaitForSeconds(0.5f);
        ParticleSystem part = Instantiate(particle, transform); // 풀링 써보기.
        AtiveObj(gameObject.transform, false);
        circle2d.enabled = true;
        yield return new WaitForSeconds(delay);
        circle2d.enabled = false;
        Destroy(part.gameObject); 
        gameObject.SetActive(false);
    }
    public void AtiveObj(Transform trans, bool isActive)
    {
        // 스프라이트 꺼주기
        for (int i = 0; i < 2; i++)
        {
            trans.GetChild(i).GetComponent<SpriteRenderer>().enabled = isActive;
        }
    }
    public void ExplosionFinish()
    {
        // 애니메이션에 넣어줄 함수 오브젝트 끄기.
        gameObject.SetActive(false);
    }
}
