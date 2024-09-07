using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHp : MonoBehaviour
{
    private Image Hp;
    [SerializeField] GameObject monster;
    [SerializeField] float curHp = 5;
    [SerializeField] float maxHp = 5;
    // Start is called before the first frame update
    [SerializeField] bool GetDamage;
    [SerializeField] GameObject item;
    Animator anim;
    Monster monsterSc;
    private void Awake()
    {
        Hp = transform.Find("Hp").GetComponent<Image>();
        
    }
    void Start()
    {
        monsterSc = monster.GetComponent<Monster>();
        anim = monster.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PosHpbar();
        checkHp();
        TestFunction_GetDamage();
    }

    private void PosHpbar()
    {
        Vector3 hpPos = monster.transform.position;//몬스터의 위치, 월드 포지션
        //Camera.main.WorldToScreenPoint(hpPos);
        hpPos.x -= 0.31f;
        hpPos.y += 0.93f;
        transform.position = hpPos;
    }
    private void checkHp()
    {
        float valueHp = curHp/maxHp;
        if(Hp.fillAmount >valueHp) 
        {
            Hp.fillAmount -= Time.deltaTime;
            if (Hp.fillAmount <= valueHp)
            {
                Hp.fillAmount = valueHp;
            }
        }
    }
    private void death()
    {
        if(Hp.fillAmount <= 0)
        {
            Destroy(gameObject);
            anim.SetBool("isDeath", true);
            Destroy(monster);
            Instantiate(item,transform.position,Quaternion.identity);
        }
    }

        

    private void TestFunction_GetDamage()
    {
        if (GetDamage == true )
        {
            
            GetDamage = false;
            Hit(1);
        }
    }

    public void Hit(float _damage)
    {

        monsterSc.Damage();
        curHp -= _damage;
        
        if (curHp <= 0)
        {
            death();           
        }
    }
}
