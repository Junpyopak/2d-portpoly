using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class items : MonoBehaviour
{
    [SerializeField] float twinkleTime = 2f;
    // Start is called before the first frame update
    bool isFill = false;
    SpriteRenderer spRenderer;
    [SerializeField] bool getitems = false;

    BoxCollider2D boxCol2;
    Rigidbody2D rigid2d;
    private bool isGround = false;
    private void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        boxCol2 = GameObject.Find("GroundBox").GetComponent<BoxCollider2D>();
        rigid2d = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        if (isFill == true && spRenderer.color.a >= 0)//드랍 되었을때 스프라이트의 알파값을 조정해서 키 위치를 나타냄
        {
            Color color = spRenderer.color;
            color.a -= Time.deltaTime / twinkleTime;
            spRenderer.color = color;

            if (spRenderer.color.a < 0)
            {
                isFill = false;
            }
        }
        else if (isFill == false && spRenderer.color.a <= 1)
        {
            Color color = spRenderer.color;
            color.a += Time.deltaTime / twinkleTime;
            spRenderer.color = color;

            if (spRenderer.color.a > 1)
            {
                isFill = true;
            }
        }
        CheckGround();
        if(isGround == true) 
        {
            rigid2d.gravityScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))//플레이어가 키를 먹었을때의 기능
        {
            getitems = true;

            Destroy(gameObject);
        }

    }

    private void CheckGround()
    {
        if (boxCol2.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGround = true;

        }
        else
        {
            isGround = false;
        }
    }
}
