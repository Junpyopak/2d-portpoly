using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    Animator anim;
    Player playerSc;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerSc = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        openGate();
    }

   private void openGate()
    {
        if (playerSc.getitem==true)
        {
            anim.SetBool("HasItem", true);
        }
    }
}
