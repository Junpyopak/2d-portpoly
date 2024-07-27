using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [Header("�÷��̾�")]
    [SerializeField] bool attack1 = false;
    [SerializeField] float moveSpeed = 3f;
    Rigidbody2D rigid2d;
    Animator anim;
    BoxCollider2D LadderBox;
    private bool isGround = false;
    Vector2 movePos;
    [SerializeField] float JumpForce = 3;

    [SerializeField] float CurHp = 20;
    [SerializeField] float MaxHp = 20;

    int curText = 0;
    int atkCount = 0;

    [Tooltip("�÷��̾� ��ٸ� Ÿ��")]
    public bool isladder = false;
    [SerializeField] float ClimeForce = 3;
    float climeSpeed = 0;

    [Tooltip("�÷��̾� �������� ���")]
    [SerializeField] public bool getitem = false;

    // Start is called before the first frame update
    [Header("�÷��̾� �̵����� ����")]
    [SerializeField, Tooltip("ȭ�� �ּҺ���")] Vector2 minScreen;
    [SerializeField, Tooltip("�ִ����")] Vector2 maxScreen;
    [SerializeField] Camera cam;

    [Header("Ʃ�丮�� ����")]
    [SerializeField] GameObject objExplanMove;
    [SerializeField] GameObject objExplanSetting;
    [SerializeField] GameObject objExplanJump;

    [Header("PlayerHp")]
    [SerializeField] PlayerHp playerHp;

    private void Awake()
    {

    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();
        if(SceneManager.GetActiveScene().name == "TutorialScene")
        {
            objExplanSetting.SetActive(false);
            objExplanJump.SetActive(false);
        }    
        LadderBox = GameObject.Find("Ladders").GetComponent<BoxCollider2D>();


    }
    private void FixedUpdate()
    {
        if (playerHp.CurHp <= 0) return;//player.hp�� hp ���� ������ 0�̶�� ����ó���ؼ� �Ʒ� �ڵ���� ����� �����ư��Բ�.

        moving();
    }
    // Update is called once per frame
    void Update()
    {
        checkPos();
        if(SceneManager.GetActiveScene().name == "TutorialScene")
        {
            ExplanMove();
        }
        // ExplanSett();
        CheckGround();
        Jumping();
        Attack();
        //checkLadder();
        //if (isladder == true && Input.GetKey(KeyCode.UpArrow))// || Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    this.gameObject.layer = 8;
        //    anim.SetBool("isClime", true);
        //    //rigid2d.gravityScale = 0;
        //    rigid2d.velocity = Vector2.up * ClimeForce;           
        //}
        //rigid2d.gravityScale = 1;
        if (isladder == true)
        {

            //float climeSpeed = Input.GetAxisRaw("Vertical");
            //anim.SetBool("isClime", true);
            //anim.SetFloat("ClimeSpeed", climeSpeed);
            this.gameObject.layer = 8;
            if (Input.GetKey(KeyCode.UpArrow))//��ٸ� �ö󰥋�
            {
                rigid2d.gravityScale = 0;
                climeSpeed = 1;
                anim.SetFloat("ClimeSpeed", climeSpeed);
                anim.SetBool("isClime", true);
                rigid2d.velocity = Vector2.up * ClimeForce;
            }
            else if (Input.GetKey(KeyCode.DownArrow))//ĳ���� ��ٸ� ������ �����Ë�
            {
                rigid2d.gravityScale = 0;
                climeSpeed = -1;
                anim.SetBool("isClime", true);
                anim.SetFloat("ClimeSpeed", climeSpeed);
                rigid2d.velocity = Vector2.down * ClimeForce;
            }
            else if (anim.GetBool("isClime") == true)
            {
                climeSpeed = 0;
                anim.SetFloat("ClimeSpeed", climeSpeed);
                rigid2d.velocity = Vector2.zero;
                //rigid2d.gravityScale = 0;
            }
        }
        //if(isladder==true && Input.GetKey(KeyCode.DownArrow))//ĳ���� ��ٸ� ������ �����Ë�
        //{
        //    this.gameObject.layer = 8;
        //    float downSpeed = -1;
        //    anim.SetBool("isClime", true);
        //    rigid2d.gravityScale = 0;
        //    anim.SetFloat("ClimeSpeed", downSpeed);
        //    rigid2d.velocity = Vector2.down * ClimeForce;
        //}
        //if (isladder ==true && Input.GetKey(KeyCode.DownArrow))
        //{float climeSpeed = Input.GetAxisRaw("Vertical");
        //    climeSpeed = -1;
        //    rigid2d.velocity = Vector2.down * ClimeForce;

        //}
        if (isladder == false)
        {
            rigid2d.gravityScale = 1;
            anim.SetBool("isClime", false);
            this.gameObject.layer = 0;
        }
    }

    private void moving()
    {
        if (isladder == true)//��ٸ� ����� ���� �Ǿ����� �÷��̾��� �¿��̵� ����(movepos�� ����)
        {
            movePos.x = 0;
        }
        if (isladder == false)
        {
            movePos.x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        }
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
        if (rigid2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
        if (isGround == true && Input.GetKeyDown(KeyCode.C))
        {
            anim.SetBool("isJump", true);
            rigid2d.velocity = Vector2.up * JumpForce;
        }

        else
        {
            anim.SetBool("isJump", false);
        }
    }

    private void StartAttck1()
    {
        attack1 = true;
    }

    private void EndAttact1()
    {
        attack1 = false;
    }

    private void EndAttack2()
    {
        anim.SetBool("Atk2", false);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (attack1 == false)
            {
                anim.SetTrigger("Atk1");
            }
            else if (anim.GetBool("Atk2") == false)
            {
                anim.SetBool("Atk2", true);
            }
        }
    }

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
        
        if (movePos.x != 0 && curText == 0)//�ؽ�Ʈ�� ����� �ʾ����鼭,�ɸ����� movepos�� 0�� �ƴҶ�
        {
            objExplanMove.SetActive(false);
            objExplanSetting.SetActive(true);
            curText = 1;//�ش� �Լ��� �ٽ� ���ư��� ���� �������� curText���� �����Ͽ� ���� �ؽ�Ʈ�� ��������.
        }
    }

    //private void checkLadder()
    //{
    //    if (rigid2d.IsTouchingLayers(LayerMask.GetMask("Ladders")))
    //    {
    //        isladder = true;
    //        if (isladder == true && Input.GetKey(KeyCode.UpArrow))// || Input.GetKeyDown(KeyCode.DownArrow))
    //        {

    //            anim.SetBool("isClime", true);
    //            rigid2d.gravityScale = 0;
    //            rigid2d.velocity = Vector2.up * ClimeForce;

    //        }
    //        //if (Input.GetKeyUp(KeyCode.UpArrow))
    //        //{
    //        //    //anim.SetFloat("ClimeSpeed", 0.0f);
    //        //}
    //        //else
    //        //{

    //        //}
    //    }
    //    else
    //    {
    //        isladder = false;
    //        rigid2d.gravityScale = 1;
    //        anim.SetBool("isClime", false);
    //        this.gameObject.layer = 0;
    //    }

    //    if (isladder == true)
    //    {
    //        this.gameObject.layer = 8;
    //        float climeSpeed = Input.GetAxisRaw("Vertical");
    //        anim.SetFloat("ClimeSpeed", climeSpeed);

    //        if(climeSpeed == -1)
    //        {
    //            rigid2d.velocity = Vector2.down * ClimeForce;
    //        }
    //    }

    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladders"))
        {
            isladder = true;
        }
        if (collision.CompareTag("keyitem"))
        {
            getitem = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladders"))
        {
            isladder = false;
        }
    }
    public void death()
    {
        anim.SetTrigger("DoDeath");
    }
}
