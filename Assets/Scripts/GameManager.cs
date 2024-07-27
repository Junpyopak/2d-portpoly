using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("아이템드롭")]
    [SerializeField, Range(0.0f, 100.0f)] float itemDropRate = 0.0f;//0.0~100.0f
    [SerializeField] GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem(Vector3 _pos)
    {      
        Instantiate(item, _pos, Quaternion.identity);
    }
}
