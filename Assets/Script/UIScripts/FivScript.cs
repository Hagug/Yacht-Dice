using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FivScript : MonoBehaviour
{
    int fivtotal;
    public TMP_Text btntext;
    public TMP_Text subTotal;
    public TMP_Text total;
    public TMP_Text bonus;
    int subTotal_int;
    int total_int;
    public static bool[] isBtnClicked = new bool[4];  //이 버튼이 이미 눌렸는지 확인하는 변수

    AudioSource audiosource;
    public AudioClip[] sucSfx = new AudioClip[8];
    public AudioClip[] failSfx = new AudioClip[10];

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        fivtotal = 0;
        isBtnClicked[0] = false;
        isBtnClicked[1] = false;
        isBtnClicked[2] = false;
        isBtnClicked[3] = false;
    }

    public void OnClickButton()
    {
        if(DiceScript.diceValue == 5)
            fivtotal += 5;
        if(Dice2Script.diceValue == 5)
            fivtotal += 5;
        if(Dice3Script.diceValue == 5)
            fivtotal += 5;
        if(Dice4Script.diceValue == 5)
            fivtotal += 5;
        if(Dice5Script.diceValue == 5)
            fivtotal += 5;

        btntext.text = fivtotal.ToString();
        GetComponent<Button>().interactable = false;
        // 점수를 넣으면 주사위 위치 초기화
        DiceScript.resetPosition = true;
        Dice2Script.resetPosition = true;
        Dice3Script.resetPosition = true;
        Dice4Script.resetPosition = true;
        Dice5Script.resetPosition = true;

        subTotal_int = int.Parse(subTotal.text);
        total_int = int.Parse(total.text);
        subTotal_int += fivtotal;
        subTotal.text = subTotal_int.ToString();
        total_int += fivtotal;
        total.text = total_int.ToString();
        if(subTotal_int >= 63) // 보너스를 받을 수 있는지 검사
            bonus.text = "35";        

        // 버튼의 눌림 여부 변경
        isBtnClicked[TurnScript.playerTurn-1] = true;

        // 게임 및 플레이어 턴 관리
        if(TurnScript.playerTurn == TurnScript.gameMode)
        {
            TurnScript.playerTurn = 1;
            TurnScript.gameTurn++;
        }
        else
        {
            TurnScript.playerTurn++;
        } 

        // RollChance 초기화
        MoveMug.rollChance = 3;

        if(fivtotal < 15)
        {
            audiosource.clip = failSfx[Random.Range(0,8)];
        }
        else
        {
            audiosource.clip = sucSfx[Random.Range(0,10)];
        }
        audiosource.Play();
    }
}
