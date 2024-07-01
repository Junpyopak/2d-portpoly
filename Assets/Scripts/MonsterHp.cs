using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHp : MonoBehaviour
{
    private Image HpBar;
    [SerializeField] GameObject monster;
    // Start is called before the first frame update

    private void Awake()
    {
        HpBar = transform.Find("Hp").GetComponent<Image>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PosHpbar();
    }

    private void PosHpbar()
    {
        Vector3 hpPos = monster.transform.position;//������ ��ġ, ���� ������
        //Camera.main.WorldToScreenPoint(hpPos);
        hpPos.x -= 0.31f;
        hpPos.y += 0.91f;
        transform.position = hpPos;
    }
}
