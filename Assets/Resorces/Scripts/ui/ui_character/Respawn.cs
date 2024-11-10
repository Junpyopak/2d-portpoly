using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
    public Transform RsPoint;//리스폰 위치
    public GameObject Player;
    //GameObject desPlayer;
    MapCam cam;
    PlayerHp playerHp;
    public GameObject Hpbar;
    private Image Hp;
    // Start is called before the first frame update
    void Start()
    {
        //am = GameObject.Find("MapCam").GetComponent<MapCam>();
        playerHp = GameObject.Find("PlayerHp").GetComponent<PlayerHp>();
        Hp = GameObject.Find("HPBar").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    static int testIndex = 0;
    public void spawn()
    {
        //Vector3 newPos = RsPoint.position;
        //GameObject goplayer = Instantiate(Player, newPos, Quaternion.identity);

        ////ryu
        //goplayer.name = $"Player {testIndex.ToString()}";
        //testIndex++;
        //cam.trsPlayer.position = goplayer.transform.position;

        Player.transform.position = RsPoint.position;
        playerHp.curHp = 20;
        Hp.fillAmount = 1;
        Hpbar.SetActive(true);     
    }

    //public void  destroy()
    //{
    //    desPlayer = GameObject.Find("Player");
    //    Destroy(desPlayer);
    //}


}
