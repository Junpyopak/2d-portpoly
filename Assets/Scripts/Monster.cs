using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    Animator anim;
    //BoxCollider2D boxCol;
    Collider2D movChBox;
    Rigidbody2D rigid;
    [SerializeField] bool GetDamage;
    [SerializeField] float speed;
    MonsterHp monsterHpSc;
    [SerializeField] bool isGround;
    [SerializeField] bool checkWAll;
    void Start()
    {
        anim = GetComponent<Animator>();
       // boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        monsterHpSc = GameObject.Find("CanvasHp").GetComponent<MonsterHp>();
        movChBox = transform.GetChild(1).GetComponent<Collider2D>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (movChBox.IsTouchingLayers(LayerMask.GetMask("wall")) == true || movChBox.IsTouchingLayers(LayerMask.GetMask("Gate")) == true)
        {
            checkWAll = true;
            flip();
        }

    }
    void Update()
    {
        CheckGround();
        Move();
        //Debug.DrawRay(transform.position, -transform.forward * 10, Color.red);//레이캐스트 충돌
        //{

        //}
    }


    public void Damage()
    {
        anim.SetTrigger("Damage");
    }

    private void Move()//몬스터 이동 ai
    {
        if (isGround == true)
        {
            anim.SetBool("isGround", true);
            rigid.velocity = new Vector2(speed, 0);

        }
        else
        {
            anim.SetBool("isGround", false);
        }

    }

    private void flip()//몬스터 이동 ai 중 벽을만나거나,낭떠러지,게이트를 만나면 뒤돌아 전진.
    {

        speed *= -1;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

    }
    private void CheckGround()//플레이어의 콜라이더가 땅에 닿아있지 않으면 플레이어에게 중력의 영향을 받을수 있도록
    {
        if (rigid.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGround = true;

        }
        else
        {
            isGround = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            monsterHpSc.Hit(1);
            Damage();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }

}
