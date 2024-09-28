using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    Animator anim;
    BoxCollider2D boxCol;
    BoxCollider2D movBox;
    Rigidbody2D rigid;
    [SerializeField] bool GetDamage;
    [SerializeField] float speed;
    MonsterHp monsterHpSc;
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        monsterHpSc = GameObject.Find("CanvasHp").GetComponent<MonsterHp>();
        movBox = GameObject.Find("moveBox").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (movBox.IsTouchingLayers(LayerMask.GetMask("Ground")) == true || movBox.IsTouchingLayers(LayerMask.GetMask("Gate")) == true)
        {
            flip();
        }

    }
    void Update()
    {
        Debug.DrawRay(transform.position, -transform.forward * 10, Color.red);//레이캐스트 충돌
        {
            
        }
    }
   

    public void Damage()
    {
        anim.SetTrigger("Damage");
    }

    private void Move()//범위에 한번이라도 들어온다면 이동.
    {
        
    }

    private void flip()//몬스터 이동 ai 중 벽을만나거나,낭떠러지,게이트를 만나면 뒤돌아 전진.
    {
        speed *= -1;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            monsterHpSc.Hit(1);
            Damage();      
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
   
}
