using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class MonsterHp : MonoBehaviour
{
    private Image Hp;
    [SerializeField] GameObject monster;
    //[SerializeField] float curHp = 10;
    //ryu
    [SerializeField] public float curHp = 10;
    [SerializeField] float maxHp = 10;
    private float deathTime = 1.45f;
    
    // Start is called before the first frame update
    
    [SerializeField] bool GetDamage;
    [SerializeField] GameObject item;

    [Header("아이템")]
    [SerializeField] bool hasItem = false;//아이템을 가지고 있는지
    public bool dropItem = false;//아이템을 드롭했는지
    Animator anim;
    Monster monsterSc;
    Spawn spawnSc;
    private void Awake()
    {
        Hp = transform.Find("Hp").GetComponent<Image>();
        
    }
    void Start()
    {
        //ryu
        monsterSc = monster.GetComponent<Monster>();
        spawnSc = GameObject.Find("SpawnPoint").GetComponent<Spawn>();
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
    private void death()//몬스터가 죽을때 ui
    {
        if(Hp.fillAmount <= 0)//체력이 다 소진하면 몬스터는 죽고
        {
            Destroy(gameObject);
            Destroy(monster);
           // Spawn.Instance.enemyCount--;//스폰 스크립트에 있는 몬스터 카운트가 --된다.
            if ((hasItem == true) && dropItem == false)//내가 아이템을 보유하고 있고 드랍하지 않았다면
            {
                dropItem = true;
                GameManager.Instance.DropItem(transform.position);
            }
        }
    }


    public void Hit(float _damage)
    {
        //solution
        //monsterSc.Damage();
        //ryu
        curHp -= _damage;
        Debug.Log($"<color='red'>damage {this.gameObject.name}</color>");


        if (curHp <= 0)
        {
            death();           
        }
    }
    public void SetHaveItem()
    {
        hasItem = true;
    }
}
