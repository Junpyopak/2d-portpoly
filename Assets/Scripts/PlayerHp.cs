using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{

    private Image Hp;
    
    [SerializeField] float curHp = 20;
    public float CurHp => curHp;//�ۿ��� �����Ҽ� ������ �б��������θ� �����ü� ����.
    [SerializeField] float maxHp = 20;
    Player player;

    [SerializeField] bool GetDamage;
    private void Awake()
    {
        Hp = transform.Find("HPBar").GetComponent<Image>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //player = gameObject.GetComponent<Player>();//null
        player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //TestFunction_GetDamage();

        checkHp();
    }

    //private void TestFunction_GetDamage()
    //{
    //    if (GetDamage == true)
    //    {
    //        GetDamage = false;

    //        Hit(1);
    //    }
    //}


    public void Hit(float _damage)
    {
        player.Damage();
        curHp -= _damage;

        if(curHp <= 0) 
        {
            curHp = 0;
            Destroy(gameObject);
            player.death();
        }
    }

    private void checkHp()
    {
        float valueHp = curHp/maxHp;
        if (Hp.fillAmount > valueHp)
        {
            Hp.fillAmount -= Time.deltaTime;
            if (Hp.fillAmount <= valueHp)
            {
                Hp.fillAmount = valueHp;
            }
        }
    }
}
