using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    Animator anim;
    BoxCollider2D boxCol;
    Rigidbody2D rigid;
    [SerializeField] bool GetDamage;

    MonsterHp monsterHpSc;
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        monsterHpSc = GameObject.Find("CanvasHp").GetComponent<MonsterHp>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   

    public void Damage()
    {
        anim.SetTrigger("Damage");
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
