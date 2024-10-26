using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform sPoint;//������ ��ġ
    public GameObject Enemy;
    public int maxEnemy = 1;
    public int enemyCount;

    public static Spawn Instance;
    [Header("�� ����")]
    [SerializeField] List<GameObject> listEnemy;//���� ����
    List<GameObject> listSpawnEnemy = new List<GameObject>();//������ ����

    [SerializeField] float spawnTime = 4.0f;
    [SerializeField] float sTimer = 0.0f;//����Ÿ�̸� 

    [Header("�� ���� ī�޶󸮹�")]
    [SerializeField] Vector2 vecCamMinMax;//��ȹ�ڰ� �����ϴ� ��ġ��, ī�޶�κ���
    Vector2 vecSpawnLimit;//���� ������ ���� ���� ���� ��ġ��, ����������

    [Header("�����۵��")]
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

    private void createEnemy()//���� �����մϴ�.
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

        //listSpawnEnemy.Add(go);//���� ����Ʈ�� ����ϴ� ���

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
    private void checkSpawn()//������ �ִ� ���� ��ŭ ���� ������
    {

        if (enemyCount < maxEnemy)
        {
            sTimer += Time.deltaTime;
            if (sTimer >= spawnTime)//�� ��ȯŸ�̸Ӱ� ���ư��� �������ð��� �Ǹ� ������
            {
                sTimer = 0.0f;
                createEnemy();//�� ������
                enemyCount++;

            }
        }
    }
}
