using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice2Script : MonoBehaviour {

	static Rigidbody rb;
	static bool hasLanded;
	public static bool thrown;
	Vector3 initPosition;
	public static int diceValue;
	public DiceSide[] diceSides;
	Vector3 MouseDownPos;
	GameObject[] Slots;
	Vector3 DicePosition;
	Vector3 upDice;
	int slotValue;
	public static bool resetPosition;
	
    // Start is called before the first frame update
    void Start () 
	{
		// 각종 변수 초기화
		rb = GetComponent<Rigidbody> ();
		initPosition = transform.position;
		rb.useGravity = true;
		thrown = false;
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
				//Debug.Log("Dice2 slotvalue : " + slotValue);
				MouseDownPos = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay(MouseDownPos);
				RaycastHit hit;
				// 광선과 충돌한 collider(RaycastHit 타입)를 hit에 넣음
				if(Physics.Raycast(ray, out hit))
				{
					if(hit.transform.gameObject.tag == "Dice2")
					{
						if(slotValue == 0)
							PutSlot(hit);
						else
							PopSlot(hit, slotValue-1);
					}
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.R) && slotValue == 0) // R을 눌렀을때 슬롯에 없다면? 처음위치(다시굴리기)로 이동
        {
			thrown = false;
			hasLanded = false;
            transform.position = initPosition;
			diceValue = 0;
        }
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
	void SideValueCheck()
	{
		diceValue = 0;
		foreach (DiceSide side in diceSides)
		{
			if(side.OnGround())
			{
				diceValue = side.sideValue;
			}
		}
	}

	void PutSlot(RaycastHit hit)
	{
		for (int i = 0; i<5; i++)
		{
			if(DiceScript.inSlot[i] == false)
			{
				// 슬롯에서 뺐을때 주사위 위치를 지정해주기 위해서 현재 주사위 위치를 저장
				DicePosition = hit.transform.position;
				// 주사위 위치를 슬롯으로 이동
				hit.transform.position = Slots[i].transform.position + upDice;
				DiceScript.inSlot[i] = true;
				break;
			}
		}
	}

	void PopSlot(RaycastHit hit, int value)
	{
		hit.transform.position = DicePosition;
		DiceScript.inSlot[value] = false;
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
