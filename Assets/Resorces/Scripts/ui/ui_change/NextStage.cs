using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        nextStage();
    }

   private void openGate()
    {
        if (playerSc.getitem==true)
        {
            anim.SetBool("HasItem", true);
        }
    }
    private void nextStage()
    {
        if(playerSc.getitem == true&&anim.GetBool("HasItem")==true)
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
