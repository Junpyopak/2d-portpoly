using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour

{

    [SerializeField] GameObject objSetting;
    [SerializeField] GameObject objKeySet;
    [SerializeField] GameObject objExplanSetting; 
    [SerializeField] GameObject objExplanJump;
    int text = 0;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
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
        if (objKeySet.activeSelf==false&&Input.GetKeyDown(KeyCode.Escape))
        {
            if(objSetting.activeSelf == false)
            {
                objSetting.SetActive(true);
                objExplanSetting.SetActive(false);
                objExplanJump.SetActive(true);
                Time.timeScale = 0;
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
            objExplanJump.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void OnclickSetting()
    {
        objSetting.SetActive(false);
        objKeySet.SetActive(true );
    }

    public void Check()
    {
        objKeySet.SetActive(false);
        objSetting.SetActive(true);
    }
}
