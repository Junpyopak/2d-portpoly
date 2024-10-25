using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform sPoint;//������ ��ġ
    public GameObject Enemy;
    public int maxEnemy =1;
    public int enemyCount;

    [Header("�� ����")]
    [SerializeField] List<GameObject> listEnemy;//���� ����
    List<GameObject> listSpawnEnemy = new List<GameObject>();//������ ����

    [SerializeField]float spawnTime = 4.0f;
    [SerializeField]float sTimer = 0.0f;//����Ÿ�̸� 

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

    private void createEnemy()//���� �����մϴ�.
    {
        GameObject objEnemy = Enemy;
        Vector3 newPos = sPoint.position;
        GameObject go = Instantiate(objEnemy, newPos, Quaternion.identity);

        //listSpawnEnemy.Add(go);//���� ����Ʈ�� ����ϴ� ���

        float rate = Random.Range(0.0f, 100.0f);
        if (rate <= itemDropRate)
        {
            MonsterHp goSc = go.GetComponent<MonsterHp>();
            goSc.SetHaveItem();
        }
    }
    private void checkSpawn()//���� ��ȯ�ص� �Ǵ��� üũ
    {
        sTimer += Time.deltaTime;
        if (sTimer >= spawnTime )
        {
            sTimer = 0.0f;
            createEnemy();//�� ������
        }
    }
    

}
