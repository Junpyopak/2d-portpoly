using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    BoxCollider2D AtkBox;
    BoxCollider2D Atk2Box;
    BoxCollider2D Atk2Box2;



    [Tooltip("�÷��̾� ��ٸ� Ÿ��")]
    public bool isladder = false;
    private bool doLadder = false;//���� ��ٸ��� �̿��ϰ� �ִ���
    [SerializeField] private bool ableDownAction = false;//idle���¿��� �������� ������� ��ȯ�Ҽ� �ִ���
    [SerializeField] private bool ableUpAction = false;//idle���¿��� �ö󰡴� ������� ��ȯ�Ҽ� �ִ���
    [SerializeField] float ClimeForce = 3;
    float climeSpeed = 0;
    BoxCollider2D ckLadder;

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

    [Tooltip("�÷��̾� ������")]
    private bool doDamage = false;

    private void Awake()
    {

    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();
        if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            objExplanSetting.SetActive(false);
            objExplanJump.SetActive(false);
        }

        AtkBox = GameObject.Find("AtkBox").GetComponent<BoxCollider2D>();//�÷��̾��� ���ݹ�������
        Atk2Box = GameObject.Find("Atk2Box").GetComponent<BoxCollider2D>();
        Atk2Box2 = GameObject.Find("Atk2Box2").GetComponent<BoxCollider2D>();

        AtkBox.enabled = false;//�÷��̾ �����ϱ⵵ ���� ������� ���̵����� �޴°� �����ϱ����� �ݶ��̴��� �������ڸ��� ��Ȱ��ȭ
        Atk2Box.enabled = false;
        Atk2Box2.enabled = false;

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
        if (SceneManager.GetActiveScene().name == "TutorialScene")
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
        doLadder = anim.GetBool("isClime");
        if (isladder == true)//��ٸ��� ���� �ߴٸ�
        {
            //float climeSpeed = Input.GetAxisRaw("Vertical");
            //anim.SetBool("isClime", true);
            //anim.SetFloat("ClimeSpeed", climeSpeed);
            if (doLadder == true)//�̹� ��ٸ��� Ÿ�� �ִ� �����϶�
            {
                if (Input.GetKey(KeyCode.UpArrow))//��ٸ� �ö󰥋�
                {
                    this.gameObject.layer = 8;
                    rigid2d.gravityScale = 0;//�÷��̾���  gravityScale�� ������ ��ٸ��� Ż�� �ֵ��� 0���� ����
                    climeSpeed = 1;
                    anim.SetFloat("ClimeSpeed", climeSpeed);//���ϸ��̼��� Ŭ���� �ӵ��� ���� ���� �޾ƿ� ���� ���� ���ǵ尡 1�̶�� �ö󰡰� -1�̶�� �������� ���԰� 0�̶�� ��ٸ����� ���ߴ� ����
                    anim.SetBool("isClime", true);//��ٸ� Ÿ�� ����
                    rigid2d.velocity = Vector2.up * ClimeForce;
                }
                else if (Input.GetKey(KeyCode.DownArrow))//ĳ���� ��ٸ� ������ �����Ë�
                {
                    this.gameObject.layer = 8;
                    rigid2d.gravityScale = 0;
                    climeSpeed = -1;
                    anim.SetBool("isClime", true);
                    anim.SetFloat("ClimeSpeed", climeSpeed);
                    rigid2d.velocity = Vector2.down * ClimeForce;
                }
                else
                {
                    climeSpeed = 0;
                    anim.SetFloat("ClimeSpeed", climeSpeed);
                    rigid2d.velocity = Vector2.zero;
                    //rigid2d.gravityScale = 0;
                }
            }
            else//��ٸ��� ��Ҵµ� ���� idle �����϶�
            {
                if (ableUpAction == true && Input.GetKeyDown(KeyCode.UpArrow) || ableDownAction == true && Input.GetKeyDown(KeyCode.DownArrow))//�׼��� �����Ҷ� ���� Ű�� �Է��Ѵٸ�
                {
                    anim.SetBool("isClime", true);
                }
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
        if (isladder == false)//��ٸ��� Ÿ�� ���� �ʴٸ�,gravityScale�� 1�� �����Ͽ� �߷��� ������ �޵��� ����
        {
            rigid2d.gravityScale = 1;
            anim.SetBool("isClime", false);
            this.gameObject.layer = 0;
        }
    }

    private void moving()
    {
        if (doLadder == true)//��ٸ� ����� ���� �Ǿ����� �÷��̾��� �¿��̵� ����(movepos�� ����)
        {
            movePos.x = 0;
        }
        else
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

    private void CheckGround()//�÷��̾��� �ݶ��̴��� ���� ������� ������ �÷��̾�� �߷��� ������ ������ �ֵ���
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

    private void StartAttck1()//�÷��̾� 2�� ������ ���� �÷��̾ ���� Ű�� �������� attack1�� Ʈ�簡 �ǰ�
    {
        attack1 = true; 
        AtkBox.enabled = true;//�÷��̾ ������ ���������� atk1�� �ڽ� ���� Ȱ��ȭ �Ͽ� ���� ������ ���������ް�
    }

    private void EndAttact1()//���� �÷��̾ ������ �����ٸ� ���� ������ ��������
    {
        attack1 = false;
        AtkBox.enabled = false;
    }

    private void EndAttack2()
    {
        anim.SetBool("Atk2", false);
        Atk2Box.enabled = false;
        Atk2Box2.enabled = false;
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
                Atk2Box.enabled = true;//�޺����� atk2������ ���� �����Ͽ� �޺� 2������ ����������� Ȱ��ȭ.
                Atk2Box2.enabled = true;
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

    public void Damage()
    {
       anim.SetTrigger("damage");         
    }

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

        if (collision.gameObject.layer == LayerMask.NameToLayer("AbleUp"))
        {
            ableUpAction = true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("AbleDown"))
        {
            ableDownAction = true;
        }

        if(collision.CompareTag("Enemy"))
        {           
            playerHp.Hit(1);
            Damage();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladders"))
        {
            isladder = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("AbleUp"))
        {
            ableUpAction = false;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("AbleDown"))
        {
            ableDownAction = false;
        }
    }
    public void death()//�÷��̾� ü�¹ٿ��� ü�¹��� ���� 0�� �ȴٸ� �÷��̾��� ü���� ���� ���� ����������.
    {
        anim.SetTrigger("DoDeath");
    }
}
