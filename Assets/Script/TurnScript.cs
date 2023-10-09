using System;  // Array.Sort
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnScript : MonoBehaviour
{
    public TMP_Text gameTurnText;  // 게임의 턴을 출력해줄 텍스트
    public static int gameTurn;  // 게임의 턴을 담은 변수 (1~12)
    public static int playerTurn;  // 플레이어의 턴을 담은 변수 (1~4)
    public static int gameMode;  // 2인~4인 게임 모드를 정하는 변수 (2~4)
    GameObject[] p1Btns;  // 플레이어1의 버튼을 담을 배열
    GameObject[] p2Btns;  // 플레이어2의 버튼을 담을 배열
    GameObject[] p3Btns;  // 플레이어3의 버튼을 담을 배열
    GameObject[] p4Btns;  // 플레이어4의 버튼을 담을 배열
    Button btn;  // 제어할 버튼을 담을 변수
    bool[][] isBtnClickedArray = new bool[12][];  // 이 버튼이 눌린적이 있는지 여부를 확인하는 가변배열
    public GameObject gameEnd;  // 게임이 종료되면 보이는 오브젝트
    public TMP_Text endText;  // 게임이 종료되면 보이는 텍스트
    public TMP_Text p1Totaltext;  // 플레이어1의 총점 텍스트
    public TMP_Text p2Totaltext;  // 플레이어2의 총점 텍스트
    public TMP_Text p3Totaltext;  // 플레이어3의 총점 텍스트
    public TMP_Text p4Totaltext;  // 플레이어4의 총점 텍스트

    void Start()
    {
        playerTurn = 1;
        gameTurn = 1;
        p1Btns = GameObject.FindGameObjectsWithTag("P1Btn");
        p2Btns = GameObject.FindGameObjectsWithTag("P2Btn");
        p3Btns = GameObject.FindGameObjectsWithTag("P3Btn");
        p4Btns = GameObject.FindGameObjectsWithTag("P4Btn");
    }

    void Update()
    {
        gameTurnText.text = gameTurn.ToString();

        isBtnClickedArray[0] = AceScript.isBtnClicked;
        isBtnClickedArray[1] = DeuScript.isBtnClicked;
        isBtnClickedArray[2] = ThrScript.isBtnClicked;
        isBtnClickedArray[3] = FouScript.isBtnClicked;
        isBtnClickedArray[4] = FivScript.isBtnClicked;
        isBtnClickedArray[5] = SixScript.isBtnClicked;
        isBtnClickedArray[6] = ChoiceScript.isBtnClicked;
        isBtnClickedArray[7] = FoKScript.isBtnClicked;
        isBtnClickedArray[8] = FuHScript.isBtnClicked;
        isBtnClickedArray[9] = SStScript.isBtnClicked;
        isBtnClickedArray[10] = LStScript.isBtnClicked;
        isBtnClickedArray[11] = YachtScript.isBtnClicked;

        // 플레이어의 턴에 따라 버튼 활성화/비활성화
        if (playerTurn == 1) 
        {
            // 1P의 버튼들 중 이미 사용된 버튼은 다시 활성화 시키지 않는다
            for (int i = 0; i < p1Btns.Length; i++)
            {
                btn = p1Btns[i].GetComponent<Button>();
                if(isBtnClickedArray[i][playerTurn-1] == true)
                    btn.interactable = false;
                else
                    btn.interactable = true;
            }
            foreach(GameObject btnObj in p2Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
            foreach(GameObject btnObj in p3Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
            foreach(GameObject btnObj in p4Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
        }

        else if (playerTurn == 2)
        {
            foreach(GameObject btnObj in p1Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
            for (int i = 0; i < p2Btns.Length; i++)
            {
                btn = p2Btns[i].GetComponent<Button>();
                if(isBtnClickedArray[i][playerTurn-1] == true)
                    btn.interactable = false;
                else
                    btn.interactable = true;
            }
            foreach(GameObject btnObj in p3Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
            foreach(GameObject btnObj in p4Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
        }

        else if (playerTurn == 3)
        {
            foreach(GameObject btnObj in p1Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
            foreach(GameObject btnObj in p2Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
            for (int i = 0; i < p3Btns.Length; i++)
            {
                btn = p3Btns[i].GetComponent<Button>();
                if(isBtnClickedArray[i][playerTurn-1] == true)
                    btn.interactable = false;
                else
                    btn.interactable = true;
            }
            foreach(GameObject btnObj in p4Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
        }

        else if (playerTurn == 4)
        {
            foreach(GameObject btnObj in p1Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
            foreach(GameObject btnObj in p2Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
            foreach(GameObject btnObj in p3Btns)
            {
                btn = btnObj.GetComponent<Button>();
                btn.interactable = false;
            }
            for (int i = 0; i < p4Btns.Length; i++)
            {
                btn = p4Btns[i].GetComponent<Button>();
                if(isBtnClickedArray[i][playerTurn-1] == true)
                    btn.interactable = false;
                else
                    btn.interactable = true;
            }
        }

        // 게임 턴이 13이 되면 게임 종료
        if (gameTurn == 13)
        {
            gameTurnText.text = "12";

            // 승자 확인하기
            string winner = GetWinner();
        
            gameEnd.SetActive(true);
            endText.text = "Winner! Player" + winner;
        }
    }

    string GetWinner()
    {
        string winner = "";

        // 총점들의 정수형을 담을 배열
        int[] totals = new int[4];
        totals[0] = int.Parse(p1Totaltext.text);
        totals[1] = int.Parse(p2Totaltext.text);
        totals[2] = int.Parse(p3Totaltext.text);
        totals[3] = int.Parse(p4Totaltext.text);

        // 플레이어들의 총점을 내림차순 정렬
        int[] totalsSort = new int[4];
        totalsSort[0] = totals[0];
        totalsSort[1] = totals[1];
        totalsSort[2] = totals[2];
        totalsSort[3] = totals[3];

        Array.Sort(totalsSort);

        // 내림차순 배열의 마지막 값이 최대 값
        for (int i = 0; i < 4; i++)
        {
            if(totalsSort[3] == totals[i])
            {
                int winner_int = i+1;
                if (winner == "")
                    winner = winner + winner_int.ToString();
                else
                    winner = winner + ", " + winner_int.ToString();
            }
        }
        return winner;
    }
}