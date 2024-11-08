using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform RsPoint;//리스폰 위치
    public GameObject Player;
    GameObject desPlayer;
    MapCam cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<MapCam>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    static int testIndex = 0;
    public void spawn()
    {
        Vector3 newPos = RsPoint.position;
        GameObject goplayer = Instantiate(Player, newPos, Quaternion.identity);

        //ryu
        goplayer.name = $"Player {testIndex.ToString()}";
        testIndex++;
        cam.trsPlayer.position = goplayer.transform.position;
    }

    public void  destroy()
    {
        desPlayer = GameObject.Find("Player");
        Destroy(desPlayer);
    }
}
