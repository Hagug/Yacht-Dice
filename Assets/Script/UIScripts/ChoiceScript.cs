using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChoiceScript : MonoBehaviour
{
    int choice;
    public TMP_Text choiceBtnText;
    public TMP_Text total;
    int total_int;
    public static bool[] isBtnClicked = new bool[4];  //이 버튼이 이미 눌렸는지 확인하는 변수

    AudioSource audiosource;
    public AudioClip[] sucSfx = new AudioClip[8];
    public AudioClip[] failSfx = new AudioClip[10];

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        choice = 0;
        isBtnClicked[0] = false;
        isBtnClicked[1] = false;
        isBtnClicked[2] = false;
        isBtnClicked[3] = false;
    }

    public void OnClickButton()
    {
        // 주사위의 눈 모두 더하기
        choice = DiceScript.diceValue + Dice2Script.diceValue + Dice3Script.diceValue + Dice4Script.diceValue + Dice5Script.diceValue;
        choiceBtnText.text = choice.ToString();
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
        total_int += choice;
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


        if(choice < 20)
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
