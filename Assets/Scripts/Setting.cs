using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour

{

    [SerializeField] GameObject objSetting;
    // Start is called before the first frame update
    void Start()
    {
        objSetting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ActiveSetting();
    }

    private void ActiveSetting()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(objSetting.activeSelf == false)
            {
                objSetting.SetActive(true);
            }
            else
            {
                objSetting.SetActive(false);
            }
        }
    }
}
