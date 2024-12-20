using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    //ryu
    [SerializeField]
    float curHP = 0f;
    [SerializeField] bool isGround;
    [SerializeField] bool checkWAll = false;

    void Start()
    {
        
        anim = GetComponent<Animator>();
        // boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        //ryu
        //solution
        //monsterHpSc = GameObject.Find("CanvasHp").GetComponent<MonsterHp>();
        monsterHpSc = this.gameObject.GetComponentInChildren<MonsterHp>();

        movChBox = transform.GetChild(0).GetComponent<Collider2D>();

        //ryu
        curHP = monsterHpSc.curHp;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (movChBox.IsTouchingLayers(LayerMask.GetMask("wall")) == true || movChBox.IsTouchingLayers(LayerMask.GetMask("Ground")) == false)
        {
            checkWAll = true;
            flip();
        }

    }
    void Update()
    {
        CheckGround();
        Move();
        //RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.forward * 10, Color.red);//레이캐스트 충돌
        //Ray ray = new Ray(transform.position,transform.forward);
        //if (Physics.Raycast(ray, out hit, 10f))
        //{
        //    Debug.Log(hit.collider.gameObject.name);
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
            anim.SetBool("isWalk", true);
            rigid.velocity = new Vector2(speed, 0);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }

    }

    private void flip()//몬스터 이동 ai 중 벽을만나거나,낭떠러지,게이트를 만나면 뒤돌아 전진.
    {

        speed *= -1;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

    }
    private void CheckGround()
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
        //ryu
        if (collision.CompareTag("Weapon"))
        {
            monsterHpSc.Hit(1);
            Damage();

            //ryu
            curHP = monsterHpSc.curHp;

            
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }

}
