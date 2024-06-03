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
        float minX = curBound.min.x + width;//x를 카메라 크기만큼 우측으로
        float maxX = curBound.max.x - width;//x를 카메라 크기만큼 좌측으로

        float minY = curBound.min.y + height;//y를 카메라 크기만큼 위로
        float maxY = curBound.max.y - height;//y를 카메라 크기만큼 아래로

        curBound.SetMinMax(new Vector3(minX, minY), new Vector3(maxX, maxY));//바운즈를 계산한 값으로 수정
    }
}
