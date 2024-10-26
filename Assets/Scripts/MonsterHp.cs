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

    [Header("������")]
    [SerializeField] bool hasItem = false;//�������� ������ �ִ���
    public bool dropItem = false;//�������� ����ߴ���
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
        //transform.position = Camera.main.WorldToScreenPoint(monsterSc.transform.position + Vector3.up);//���� ��ġ�� ���� hp �� �̹��� ui�� ���� �̵�

    }

    private void PosHpbar()
    {
        Vector3 hpPos = monster.transform.position;//������ ��ġ, ���� ������
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
    private void death()//���Ͱ� ������ ui
    {
        if(Hp.fillAmount <= 0)//ü���� �� �����ϸ� ���ʹ� �װ�
        {
            Destroy(gameObject);
            Destroy(monster);
           // Spawn.Instance.enemyCount--;//���� ��ũ��Ʈ�� �ִ� ���� ī��Ʈ�� --�ȴ�.
            if ((hasItem == true) && dropItem == false)//���� �������� �����ϰ� �ְ� ������� �ʾҴٸ�
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
