using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class MonsterHp : MonoBehaviour
{
    public Transform itemPOS;
    private Image Hp;
    [SerializeField] GameObject monster;
    //[SerializeField] float curHp = 10;
    //ryu
    [SerializeField] public float curHp = 10;
    [SerializeField] float maxHp = 10;
    private float deathTime = 1.45f;

    // Start is called before the first frame update

    [SerializeField] bool GetDamage;
   
    [Header("아이템")]
    public GameObject item;
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
        hpPos.x += 0.27f;
        hpPos.y += 0.93f;
        transform.position = hpPos;
    }
    private void checkHp()
    {
        float valueHp = curHp / maxHp;
        if (Hp.fillAmount > valueHp)
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
        if (Hp.fillAmount <= 0)//체력이 다 소진하면 몬스터는 죽고
        {
            int ranItem = Random.Range(0, 10);
            if(ranItem<5)//50프로확률로 아이템 안나옴
            {
                Debug.Log("아이템이 안나왔습니다.");
            }
            else if(ranItem<8)//30프로확률로 아이템드랍
            {
                Instantiate(item,transform.position, Quaternion.identity);
                Debug.Log("아이템이 나왔습니다.");
            }
            else if(ranItem < 9)//10프로확률로 나중에 넣을 아이템 드랍을 위한 코드
            {
                Instantiate(item, transform.position, Quaternion.identity);
                Debug.Log("아이템이 나왔습니다.");
            }
            else if(ranItem < 10)//
            {
                Instantiate(item, transform.position, Quaternion.identity);
                Debug.Log("아이템이 나왔습니다.");
            }
            Destroy(gameObject);
            Destroy(monster);
            spawnSc.enemyCount--;//스폰 스크립트에 있는 몬스터 카운트가 --된다.
            //if ((hasItem == true) && dropItem == false)//내가 아이템을 보유하고 있고 드랍하지 않았다면
            //{
            //    dropItem = true;
            //    Vector3 dropPos = itemPOS.transform.position;
            //    GameManager.Instance.DropItem(dropPos);
            //}

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
}
