using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    Rigidbody2D rigid2d;
    Animator anim;
    public bool isGround = false;
    Vector2 movePos;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moving();
        CheckGround();
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
    }
}
