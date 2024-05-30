using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour

{

    [SerializeField] GameObject objSetting;
    [SerializeField] GameObject objKeySet;
    // Start is called before the first frame update
    void Start()
    {
        objSetting.SetActive(false);
        objKeySet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ActiveSetting();
    }

    public void ActiveSetting()
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

    public void MainonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void ResmueClick()
    {
        if(objSetting.activeSelf == true)
        {
            objSetting.SetActive(false);
        }
    }

    public void OnclickSetting()
    {
        objSetting.SetActive(false);
        objKeySet.SetActive(true );
    }
}
