using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    Rigidbody2D rigid2d;
    Animator anim;
    public bool isGround = false;
    Vector2 movePos;
    [SerializeField] float JumpForce = 3;
    // Start is called before the first frame update

    [SerializeField, Tooltip("화면 최소비율")] Vector2 minScreen;
    [SerializeField, Tooltip("최대비율")] Vector2 maxScreen;
    [SerializeField] Camera cam;
    BoxCollider2D boxColl;

    private void Awake()
    {
        
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();
        boxColl = GameObject.Find("MapCam").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moving();
        //checkPos();
        CheckGround();
        Jumping();
        Attack();
    }

    private void moving()
    {
        movePos.x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        movePos.y = Input.GetAxisRaw("Vertical");
        transform.position = new Vector2(transform.position.x + movePos.x, transform.position.y);
        anim.SetBool("isRun", movePos.x != 0);
        if (movePos.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1);
        }
        if (movePos.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1);
        }
    }

    private void CheckGround()
    {
        if(rigid2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
     private void Jumping()
    {
        if(isGround == true && Input.GetKeyDown(KeyCode.C))
        {
            anim.SetBool("isJump", true);
            rigid2d.velocity = Vector2.up * JumpForce;
        }
        else
        {
            anim.SetBool("isJump", false);
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            //movePos.x = 0;
            anim.SetBool("Atk", true);
            if (Input.GetKeyDown(KeyCode.V))
            {
                anim.SetBool("Atk2", true);
            }


        }
        else
        {
            anim.SetBool("Atk", false);
            anim.SetBool("Atk2", false);

        }
    }

    private void Attack2()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            anim.SetBool("Atk2", true);
        }
        else
        {
            anim.SetBool("Atk2", false);
        }
    }

    //private void Atk2()
    //{
    //    if (Input.GetKeyDown(KeyCode.V)&& Input.GetKeyDown(KeyCode.V))
    //    {
    //        anim.SetBool("attk2", true);
    //    }
    //    else
    //    {
    //        anim.SetBool("attk2", false);
    //    }
    //}

    //private void checkPos()
    //{
    //    Vector2 curPos = cam.WorldToViewportPoint(transform.position);
    //    if (curPos.x < minScreen.x)
    //    {
    //        curPos.x = minScreen.x;
    //    }
    //    else if (curPos.x > maxScreen.x)
    //    {
    //        curPos.x = maxScreen.x;
    //    }

    //    if (curPos.y < minScreen.y)
    //    {
    //        curPos.y = minScreen.y;
    //    }
    //    else if (curPos.y > maxScreen.y)
    //    {
    //        curPos.y = maxScreen.y;
    //    }

    //    Vector3 fixedPos = cam.ViewportToWorldPoint(curPos);
    //    transform.position = fixedPos;
    //}

    //private void checkPosi()
    //{

    //}

    private void checkPos2()
    {
        
    }
}
