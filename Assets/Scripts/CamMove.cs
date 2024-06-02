using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    Camera camMain;
    [SerializeField] float camSpeed = 1f;
    [SerializeField] GameObject player;
    Vector3 camPos;
    // Start is called before the first frame update
    void Start()
    {
        camMain = GetComponent<Camera>();
        //transform.position = new Vector3(0,0,-10);
    }

    // Update is called once per frame
    void Update()
    {
        camMove();
    }

    private void camMove()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3.1f, -10f) * camSpeed;
    }
}
