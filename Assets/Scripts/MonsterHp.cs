using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class MonsterHp : MonoBehaviour
{
    private Image Hp;
    [SerializeField] GameObject monster;
    [SerializeField] float curHp = 10;
    [SerializeField] float maxHp = 10;
    private float deathTime = 1.45f;
    
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
        //transform.position = Camera.main.WorldToScreenPoint(monsterSc.transform.position + Vector3.up);//몬스터 위치에 따라 hp 바 이미지 ui가 같이 이동

    }

    private void PosHpbar()
    {
        Vector3 hpPos = monster.transform.position;//몬스터의 위치, 월드 포지션
        //Camera.main.WorldToScreenPoint(hpPos);
        hpPos.x += 0.21f;
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
            Destroy(monster);
            Instantiate(item,transform.position,Quaternion.identity);
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
