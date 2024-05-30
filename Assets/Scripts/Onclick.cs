using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Onclick : MonoBehaviour
{

    public void onclick()
    {
        switch(this.gameObject.name)
        {
            case "Exit":
                SceneManager.LoadScene(1);
                break;

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
