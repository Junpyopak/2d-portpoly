using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCam : MonoBehaviour
{
    Camera camMain;

    [SerializeField] Bounds curBound;
    [SerializeField] BoxCollider2D boxColl;

    [SerializeField] Transform trsPlayer;
    [SerializeField] float camSpeed = 1;

    void Start()
    {
        camMain = Camera.main;
        checkMapCam();
    }

    // Update is called once per frame
    void Update()
    {
        if (trsPlayer == null)
        {
            return;
        }

        camMain.transform.position = new Vector3(
            Mathf.Clamp(trsPlayer.transform.position.x, curBound.min.x, curBound.max.x),
            Mathf.Clamp(trsPlayer.transform.position.y, curBound.min.y, curBound.max.y),
            camMain.transform.position.z)*camSpeed;
    }

    private void checkMapCam()
    {
        float height = camMain.orthographicSize;
        float width = height * camMain.aspect;

        curBound = boxColl.bounds;
        float minX = curBound.min.x + width;//x�� ī�޶� ũ�⸸ŭ ��������
        float maxX = curBound.max.x - width;//x�� ī�޶� ũ�⸸ŭ ��������

        float minY = curBound.min.y + height;//y�� ī�޶� ũ�⸸ŭ ����
        float maxY = curBound.max.y - height;//y�� ī�޶� ũ�⸸ŭ �Ʒ���

        curBound.SetMinMax(new Vector3(minX, minY), new Vector3(maxX, maxY));//�ٿ�� ����� ������ ����
    }
}
