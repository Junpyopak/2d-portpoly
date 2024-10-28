using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("아이템드롭")]
    [SerializeField, Range(0.0f, 100.0f)] float itemDropRate = 0.0f;//0.0~100.0f
    [SerializeField] List<GameObject> listItem;
    public static GameManager Instance;
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
        int raniNum = Random.Range(0, listItem.Count);//0~리스트에 있는 숫자까지
        GameObject obj = listItem[0];
        Instantiate(obj, _pos, Quaternion.identity);
    }

}
