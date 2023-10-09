using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuHScript : MonoBehaviour
{
    int full_house;
    int[] diceValues = new int[5];
    public TMP_Text fuhBtnText;
    public TMP_Text total;
    int total_int;
    public static bool[] isBtnClicked = new bool[4];  //이 버튼이 이미 눌렸는지 확인하는 변수

    AudioSource audiosource;
    public AudioClip[] sucSfx = new AudioClip[8];
    public AudioClip[] failSfx = new AudioClip[10];

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        full_house = 0;
        isBtnClicked[0] = false;
        isBtnClicked[1] = false;
        isBtnClicked[2] = false;
        isBtnClicked[3] = false;
    }

    public void OnClickButton()
    {
        // 주사위의 눈이 Full House인지 검사
        // 주사위 눈들을 배열에 넣고 오름차순 정렬
        diceValues[0] = DiceScript.diceValue;
        diceValues[1] = Dice2Script.diceValue;
        diceValues[2] = Dice3Script.diceValue;
        diceValues[3] = Dice4Script.diceValue;
        diceValues[4] = Dice5Script.diceValue;
        Array.Sort(diceValues);

        // 정렬된 값들 중 1,2가 같고 3,4,5가 같으면 풀하우스
        if( (diceValues[0] ==  diceValues[1]) && (diceValues[2] == diceValues[3] && diceValues[3] == diceValues[4]) ) 
        {
            full_house = diceValues[0] + diceValues[1] + diceValues[2] + diceValues[3] + diceValues[4];
        }
         // 정렬된 값들 중 1,2,3이 같고 4,5가 같으면 풀하우스
        else if( (diceValues[0] ==  diceValues[1] && diceValues[1] == diceValues[2]) && ( diceValues[3] == diceValues[4]) )
        {
            full_house = diceValues[0] + diceValues[1] + diceValues[2] + diceValues[3] + diceValues[4];
        }
        else
        {
            full_house = 0;
        }
        fuhBtnText.text = full_house.ToString();
        
        // 버튼 비활성화
        GetComponent<Button>().interactable = false;

        // 점수를 넣으면 주사위 위치 초기화
        DiceScript.resetPosition = true;
        Dice2Script.resetPosition = true;
        Dice3Script.resetPosition = true;
        Dice4Script.resetPosition = true;
        Dice5Script.resetPosition = true;

        // 현재 점수판에 있는 점수 가져오기(정수 변환)
        total_int = int.Parse(total.text);
        // 점수 합산하기
        total_int += full_house;
        total.text = total_int.ToString();

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

        if(full_house < 18)
        {
            audiosource.clip = failSfx[UnityEngine.Random.Range(0,8)];
        }
        else
        {
            audiosource.clip = sucSfx[UnityEngine.Random.Range(0,10)];
        }
        audiosource.Play();
    }
}
