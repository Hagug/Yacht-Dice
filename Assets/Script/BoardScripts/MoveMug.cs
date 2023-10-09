using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveMug : MonoBehaviour
{
    float rotationTime = 3.0f;
    float rotationTimer = 0;
    bool rotate = false;
    Rigidbody mugRb;  // 머그컵의 리지드바디
    Vector3 mugDir;  // 머그컵의 이동 방향
    float shakeTime = 0.1f;  // 머그컵을 이동시킬 시간
    float shakeTimer = 0;  // 머그컵 이동 타이머
    bool shake = false;  // 머그컵 이동 트리거
    float shakeSpeed = 0.5f;  // 머그컵 이동 속도
    int shakeCount = 0;
    bool shakeDelay = false;
    float delayTime = 1.0f; 
    float delayTimer = 0; 
    public static int rollChance;  // 
    public TMP_Text rollChanceText;

    public AudioSource shakeAudioSource;
    public AudioClip shakeAudioClip;

    void Start()
    {
        mugRb = GetComponent<Rigidbody>();
        mugDir = Vector3.zero;
        rollChance = 3;
    }

    void Update()
    {
        rollChanceText.text = "Roll Chance : " + rollChance.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(rotate == false)
            {
                if (rollChance > 0) 
                {
                    rotate = true;
                    rotationTimer = rotationTime;
                    shakeCount = 0;
                    rollChance--;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if(shake == false && shakeCount < 7)
            {
                shake = true;
                shakeTimer = shakeTime;
                shakeCount++;
                shakeAudioSource.PlayOneShot(shakeAudioClip);
            }
            if (shakeDelay == false && shakeCount == 7)
            {
                shakeDelay = true;
                delayTimer = delayTime;
            }
        }

        if(shakeDelay)
        {
            delayTimer -= Time.deltaTime;
            if(delayTimer < 0)
            {
                shakeCount = 0;
                shakeDelay = false;
            }
        }
    }

    void FixedUpdate()
    {
        if(rotate)
        {
            rotationTimer -= Time.deltaTime;
            if(rotationTimer < 2.6 && rotationTimer > 1.8)
            {
                mugRb.rotation = mugRb.rotation * Quaternion.Euler(0f, 0f, -145.0f*Time.deltaTime);
            }
            else if(rotationTimer < 1.8 && rotationTimer > 0.8)
            {
                DiceScript.RollDice();
                Dice2Script.RollDice();
                Dice3Script.RollDice();
                Dice4Script.RollDice();
                Dice5Script.RollDice();
            }
            else if(rotationTimer < 0.8 && rotationTimer > 0)
            {
                mugRb.rotation = mugRb.rotation * Quaternion.Euler(0f, 0f, 145.0f*Time.deltaTime);
            }
            else if(rotationTimer < 0)
            {
                rotate = false;
            }
        }
        if(shake)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer > 0.05)
            {
                mugDir.x = -1;
                transform.forward = mugDir;
                mugRb.MovePosition(mugRb.position - mugDir * shakeSpeed);
            }
            else if(shakeTimer < 0.05 && shakeTimer > 0)
            {
                mugDir.x = -1;
                transform.forward = mugDir;
                mugRb.MovePosition(mugRb.position + mugDir * shakeSpeed);
            }
            else if(shakeTimer < 0)
            {
                shake = false;
            }
        }
    }
}


