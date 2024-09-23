using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [Header("플레이어")]
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



    [Tooltip("플레이어 사다리 타기")]
    public bool isladder = false;
    private bool doLadder = false;//현재 사다리를 이용하고 있는지
    [SerializeField] private bool ableDownAction = false;//idle상태에서 내려가는 기능으로 전환할수 있는지
    [SerializeField] private bool ableUpAction = false;//idle상태에서 올라가는 기능으로 전환할수 있는지
    [SerializeField] float ClimeForce = 3;
    float climeSpeed = 0;
    BoxCollider2D ckLadder;

    [Tooltip("플레이어 스테이지 통과")]
    [SerializeField] public bool getitem = false;

    // Start is called before the first frame update
    [Header("플레이어 이동영역 제한")]
    [SerializeField, Tooltip("화면 최소비율")] Vector2 minScreen;
    [SerializeField, Tooltip("최대비율")] Vector2 maxScreen;
    [SerializeField] Camera cam;

    [Header("튜토리얼 설명")]
    [SerializeField] GameObject objExplanMove;
    [SerializeField] GameObject objExplanSetting;
    [SerializeField] GameObject objExplanJump;

    [Header("PlayerHp")]
    [SerializeField] PlayerHp playerHp;

    [Tooltip("플레이어 데미지")]
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

        AtkBox = GameObject.Find("AtkBox").GetComponent<BoxCollider2D>();//플레이어의 공격범위설정
        Atk2Box = GameObject.Find("Atk2Box").GetComponent<BoxCollider2D>();
        Atk2Box2 = GameObject.Find("Atk2Box2").GetComponent<BoxCollider2D>();

        AtkBox.enabled = false;//플레이어가 공격하기도 전에 닿았을떄 적이데미지 받는걸 방지하기위해 콜라이더를 시작하자마자 비활성화
        Atk2Box.enabled = false;
        Atk2Box2.enabled = false;

    }
    private void FixedUpdate()
    {
        if (playerHp.CurHp <= 0) return;//player.hp에 hp 값을 가져와 0이라면 리턴처리해서 아래 코드들의 기능이 못돌아가게끔.

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
        if (isladder == true)//사다리에 접촉 했다면
        {
            //float climeSpeed = Input.GetAxisRaw("Vertical");
            //anim.SetBool("isClime", true);
            //anim.SetFloat("ClimeSpeed", climeSpeed);
            if (doLadder == true)//이미 사다리를 타고 있는 상태일때
            {
                if (Input.GetKey(KeyCode.UpArrow))//사다리 올라갈떄
                {
                    this.gameObject.layer = 8;
                    rigid2d.gravityScale = 0;//플레이어의  gravityScale을 변경해 사다리를 탈수 있도록 0으로 변경
                    climeSpeed = 1;
                    anim.SetFloat("ClimeSpeed", climeSpeed);//에니메이션의 클라임 속도에 대한 값을 받아와 만약 에님 스피드가 1이라면 올라가고 -1이라면 내려가는 에님과 0이라면 사다리에서 멈추는 에님
                    anim.SetBool("isClime", true);//사다리 타기 에님
                    rigid2d.velocity = Vector2.up * ClimeForce;
                }
                else if (Input.GetKey(KeyCode.DownArrow))//캐릭터 사다리 위에서 내려올떄
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
            else//사다리에 닿았는데 현재 idle 상태일때
            {
                if (ableUpAction == true && Input.GetKeyDown(KeyCode.UpArrow) || ableDownAction == true && Input.GetKeyDown(KeyCode.DownArrow))//액션이 가능할때 지정 키를 입력한다면
                {
                    anim.SetBool("isClime", true);
                }
            }
        }
        //if(isladder==true && Input.GetKey(KeyCode.DownArrow))//캐릭터 사다리 위에서 내려올떄
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
        if (isladder == false)//사다리를 타고 있지 않다면,gravityScale은 1로 변경하여 중력의 영향을 받도록 설정
        {
            rigid2d.gravityScale = 1;
            anim.SetBool("isClime", false);
            this.gameObject.layer = 0;
        }
    }

    private void moving()
    {
        if (doLadder == true)//사다리 등반이 시작 되었을때 플레이어의 좌우이동 금지(movepos값 생각)
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

    private void CheckGround()//플레이어의 콜라이더가 땅에 닿아있지 않으면 플레이어에게 중력의 영향을 받을수 있도록
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

    private void StartAttck1()//플레이어 2단 공격을 위해 플레이어가 공격 키를 눌렀을때 attack1이 트루가 되고
    {
        attack1 = true; 
        AtkBox.enabled = true;//플레이어가 공격을 시작했을때 atk1의 박스 콜을 활성화 하여 적이 닿으면 데미지를받게
    }

    private void EndAttact1()//만약 플레이어가 공격이 끝났다면 어택 에님을 끊기위해
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
                Atk2Box.enabled = true;//콤보공격 atk2범위를 따로 설정하여 콤보 2공격이 실행됐을때만 활성화.
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

        if (movePos.x != 0 && curText == 0)//텍스트를 띄우지 않았으면서,케릭터의 movepos가 0이 아닐때
        {
            objExplanMove.SetActive(false);
            objExplanSetting.SetActive(true);
            curText = 1;//해당 함수가 다시 돌아가는 것을 막기위해 curText겂을 증가하여 다음 텍스트가 나오도록.
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
    public void death()//플레이어 체력바에서 체력바의 값이 0이 된다면 플레이어의 체력이 다해 죽음 에님을실행.
    {
        anim.SetTrigger("DoDeath");
    }
}
