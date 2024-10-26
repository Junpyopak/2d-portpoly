using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform sPoint;//리스폰 위치
    public GameObject Enemy;
    public int maxEnemy = 1;
    public int enemyCount;

    public static Spawn Instance;
    [Header("적 생성")]
    [SerializeField] List<GameObject> listEnemy;//적의 종류
    List<GameObject> listSpawnEnemy = new List<GameObject>();//생성된 적들

    [SerializeField] float spawnTime = 4.0f;
    [SerializeField] float sTimer = 0.0f;//스폰타이머 

    [Header("적 생성 카메라리밋")]
    [SerializeField] Vector2 vecCamMinMax;//기획자가 설정하는 위치값, 카메라로부터
    Vector2 vecSpawnLimit;//월드 포지션 기준 생성 리밋 위치값, 월드포지션

    [Header("아이템드롭")]
    [SerializeField, Range(0.0f, 100.0f)] float itemDropRate = 0.0f;//0.0~100.0f
    [SerializeField] GameObject item;
    private void Start()
    {


    }
    private void Update()
    {
        checkSpawn();
    }


    //ryu
    static int testIndex = 0;

    private void createEnemy()//적을 스폰합니다.
    {

        //GameObject objEnemy = Enemy;
        //Vector3 newPos = sPoint.position;
        //GameObject go = Instantiate(objEnemy, newPos, Quaternion.identity);

        //ryu
        
        Vector3 newPos = sPoint.position;
        GameObject go = Instantiate(Enemy, newPos, Quaternion.identity);

        //ryu
        go.name = $"skeleton {testIndex.ToString()}";
        testIndex++;

        //listSpawnEnemy.Add(go);//직접 리스트에 등록하는 방법

        float rate = Random.Range(0.0f, 100.0f);
        if (rate <= itemDropRate)
        {
            //solution
            //MonsterHp goSc = go.GetComponent<MonsterHp>();
            //ryu
            MonsterHp goSc = go.GetComponentInChildren<MonsterHp>();
            goSc.SetHaveItem();
        }


    }
    private void checkSpawn()//설정된 최대 적수 많큼 적을 리스폰
    {

        if (enemyCount < maxEnemy)
        {
            sTimer += Time.deltaTime;
            if (sTimer >= spawnTime)//적 소환타이머가 돌아가고 리스폰시간이 되면 리스폰
            {
                sTimer = 0.0f;
                createEnemy();//적 리스폰
                enemyCount++;

            }
        }
    }
}
