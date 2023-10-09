using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	static Rigidbody rb; // 주사위의 리지드바디
	static bool hasLanded; // 주사위가 땅에 도달했는지 확인하는 변수
	static bool thrown; // 주사위가 던져졌는지 확인하는 변수
	Vector3 initPosition; // 주사위의 초기 위치
	public static int diceValue; // 주사위의 눈을 담을 변수
	public DiceSide[] diceSides; // 주사위에 포함된 Side 들을 담을 리스트
	public static bool[] inSlot;
	Vector3 MouseDownPos;  // 마우스 클릭 위치
	GameObject[] Slots;
	Vector3 DicePosition;
	Vector3 upDice;
	int slotValue;
	public static bool resetPosition;  // 점수판에 점수를 넣었을때 주사위 위치 전체 초기화

    // Start is called before the first frame update
    void Start () 
	{
		// 각종 변수 초기화
		rb = GetComponent<Rigidbody> ();
		initPosition = transform.position;
		rb.useGravity = true;
		thrown = false;
		inSlot = new bool[] {false, false, false, false, false};
		DicePosition = new Vector3(0f,0f,0f);
		upDice = new Vector3(0f,0.91f,0f); //주사위의 위치를 옮겼을때 튀어오르게하지 않기 위해 살짝 위치조정
		slotValue = 0;
		resetPosition = false;

		Slots = new GameObject[5];
		Slots[0] = GameObject.FindWithTag("Slot1");
		Slots[1] = GameObject.FindWithTag("Slot2");
		Slots[2] = GameObject.FindWithTag("Slot3");
		Slots[3] = GameObject.FindWithTag("Slot4");
		Slots[4] = GameObject.FindWithTag("Slot5");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (rb.IsSleeping() && !hasLanded && thrown)
		{
			hasLanded = true;
			SideValueCheck();
		}
		if (rb.IsSleeping() && hasLanded && thrown)
		{
			if (Input.GetMouseButtonDown(0))
			{
				MouseDownPos = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay(MouseDownPos);
				RaycastHit hit;
				// 광선과 충돌한 collider(RaycastHit 타입)를 hit에 넣음
				if(Physics.Raycast(ray, out hit))
				{
					if(hit.transform.gameObject.tag == "Dice1")
					{
						if(slotValue == 0) // 현재 주사위의 상태가 슬롯위에 있지 않다면? 슬롯에 추가
							PutSlot(hit);
						else // 슬롯위에 있다면 슬롯에서 제거
							PopSlot(hit, slotValue-1);
					}
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.R) && slotValue == 0) // R을 눌렀을때 슬롯에 없다면? 처음위치(다시굴리기)로 이동
        {
			ResetDice();
        }
		// 두 조건문의 차이점 : 아래거는 숫자판에 값을 넣었을때 실행. 슬롯보드에 무관하게 전체 초기화. 위에거는 슬롯보드는 저장
		if (resetPosition)
		{
			thrown = false;
			hasLanded = false;
            transform.position = initPosition;
			resetPosition = false;
			diceValue = 0;
			for (int i = 0; i<5; i++)
				DiceScript.inSlot[i] = false;
		}
	}

	public static void RollDice() 
	{
		if(!thrown && !hasLanded) 
		{
			thrown = true;
			rb.AddForce(Random.Range (200, 800), 0, Random.Range (2500, 4500));
		}
 	}

    void ResetDice()
    {
        thrown = false;
		hasLanded = false;
        transform.position = initPosition;
		diceValue = 0;
    }

	void SideValueCheck()
	{
		diceValue = 0;
		foreach (DiceSide side in diceSides)
		{
			if(side.OnGround())
			{
				diceValue = side.sideValue;
				// Debug.Log("Dice1 " + diceValue + " has been rolled!");
			}
		}
	}

	void PutSlot(RaycastHit hit)
	{
		for (int i = 0; i<5; i++)
		{
			if(inSlot[i] == false)
			{
				// 슬롯에서 뺐을때 주사위 위치를 지정해주기 위해서 현재 주사위 위치를 저장
				DicePosition = hit.transform.position;
				// 주사위 위치를 슬롯으로 이동
				hit.transform.position = Slots[i].transform.position + upDice;
				inSlot[i] = true;
				break;
			}
		}
	}

	void PopSlot(RaycastHit hit, int value)
	{
		hit.transform.position = DicePosition;
		inSlot[value] = false;
	}

	void OnTriggerStay(Collider col)
    {
		if(col.CompareTag("Slot1"))
			slotValue = 1;
        else if(col.CompareTag("Slot2"))
            slotValue = 2;
        else if(col.CompareTag("Slot3"))
            slotValue = 3;
        else if(col.CompareTag("Slot4"))
            slotValue = 4;
        else if(col.CompareTag("Slot5"))
            slotValue = 5;        
        else
            slotValue = 0;
	}
}
