using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    Animator anim;
    BoxCollider2D boxCol;
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Damage()
    {
        anim.SetTrigger("Damage");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("weapon"))
        {
            Damage();
        }
    }
}
