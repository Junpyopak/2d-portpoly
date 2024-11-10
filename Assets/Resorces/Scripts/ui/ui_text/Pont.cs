using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pont : MonoBehaviour
{
    [SerializeField] TMP_Text pont_text;
    bool isFill = false;//true 가 되면 alpha를 줄여야함, false 채워야함
    [SerializeField] float twinkleTime = 2f;
    void Update()
    {
        if (isFill == true && pont_text.color.a >= 0)
        {
            Color color = pont_text.color;
            color.a -= Time.deltaTime / twinkleTime;
            pont_text.color = color;

            if (pont_text.color.a < 0)
            {
                isFill = false;
            }
        }
        else if (isFill == false && pont_text.color.a <= 1)
        {
            Color color = pont_text.color;
            color.a += Time.deltaTime / twinkleTime;
            pont_text.color = color;

            if (pont_text.color.a > 1)
            {
                isFill = true;
            }
        }
    }
}
