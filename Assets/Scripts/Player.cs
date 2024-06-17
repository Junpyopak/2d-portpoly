using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] float moveSpeed = 3f;
    Rigidbody2D rigid2d;
    Animator anim;
    BoxCollider2D LadderBox;
    private bool isGround = false;
    Vector2 movePos;
    [SerializeField] float JumpForce = 3;
    private bool isladder = false;


    // Start is called before the first frame update
    [Header("플레이어 이동영역 제한")]
    [SerializeField, Tooltip("화면 최소비율")] Vector2 minScreen;
    [SerializeField, Tooltip("최대비율")] Vector2 maxScreen;
    [SerializeField] Camera cam;

    [Header("튜토리얼 설명")]
    [SerializeField] GameObject objExplanMove;
    [SerializeField] GameObject objExplanSetting;
    [SerializeField] GameObject objExplanJump;
    int curText = 0;
    private void Awake()
    {
        
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();
        objExplanSetting.SetActive(false);
        objExplanJump.SetActive(false);
        LadderBox = GameObject.Find("Ladders").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moving();
        checkPos();
        ExplanMove();

        // ExplanSett();
        CheckGround();
        Jumping();
        Attack();
        checkLadder();
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

    private void checkPos()
    {
        Vector2 curPos = cam.WorldToViewportPoint(transform.position);
        if (curPos.x < minScreen.x)
        {
            curPos.x = minScreen.x;
        }
        else if (curPos.x > maxScreen.x)
        {
            curPos.x = maxScreen.x;
        }

        if (curPos.y < minScreen.y)
        {
            curPos.y = minScreen.y;
        }
        else if (curPos.y > maxScreen.y)
        {
            curPos.y = maxScreen.y;
        }

        Vector3 fixedPos = cam.ViewportToWorldPoint(curPos);
        fixedPos.z = 0;
        transform.position = fixedPos;
    }

    private void ExplanMove()
    {
        if (movePos.x != 0 && curText == 0)//텍스트를 띄우지 않았으면서,케릭터의 movepos가 0이 아닐때
        {
            objExplanMove.SetActive(false);
            objExplanSetting.SetActive(true);
            curText = 1;//해당 함수가 다시 돌아가는 것을 막기위해 curText겂을 증가하여 다음 텍스트가 나오도록.
        }
    }

    private void checkLadder()
    {
        if (rigid2d.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            isladder = true;
            if (isladder == true && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                anim.SetBool("isClime", true);
            }
            else
            {
                isladder = false;
                anim.SetBool("isClime", false);
            }
        }
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.name =="Ladders")
    //    {
    //        isladder = true;
    //        if (isladder == true && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
    //        {
    //            anim.SetBool("isClime", true);
    //        }
    //        else
    //        {
    //            isladder = false;
    //            anim.SetBool("isClime", false);
    //        }
    //    }
    //}
}
