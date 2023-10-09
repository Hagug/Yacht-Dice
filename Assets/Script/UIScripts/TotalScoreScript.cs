using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TotalScoreScript : MonoBehaviour
{
    int total_int;
    bool gotten_bonus;
    public TMP_Text total;
    public TMP_Text bonus;

    void Start()
    {
        gotten_bonus = false;
    } 


    // Update is called once per frame
    void Update()
    {
        if(bonus.text == "35" && gotten_bonus == false)
        {
            total_int = int.Parse(total.text);
            total_int += 35;
            gotten_bonus = true;
            total.text = total_int.ToString();
        }
    }
}
