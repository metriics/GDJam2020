﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 curMoveVector = new Vector3();
    [SerializeField]
    public float speed = 3.2f;
    

    bool isBeingKnocked = false;
    bool isFacingRight = true;
    bool isAttacking = false;
    float knockbackTime = 0.0f;
    float attackTime = 0.0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        GameEvents.current.onEnemyAttack += Knockback;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingRight)
        {
            this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else
        {
            this.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        if (isBeingKnocked)
        {
            if (knockbackTime >= 0.5f)
            {
                knockbackTime = 0.0f;
                isBeingKnocked = false;
            }
            else
            {
                knockbackTime += Time.deltaTime;
            }
        }
        else
        {
            curMoveVector = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                curMoveVector += new Vector3(0.0f, 1.0f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                curMoveVector += new Vector3(-1.0f, 0.0f);
                isFacingRight = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                curMoveVector += new Vector3(0.0f, -1.0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                curMoveVector += new Vector3(1.0f, 0.0f);
                isFacingRight = true;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isAttacking = true;
            }

            if (isAttacking)
            {
                attackTime += Time.deltaTime;
                transform.GetChild(0).gameObject.SetActive(true);

                if (attackTime >= 0.1)
                {
                    isAttacking = false;
                    attackTime = 0.0f;
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }

        controller.Move(curMoveVector * speed * Time.deltaTime);
    }

    void Knockback()
    {
        isBeingKnocked = true;
    }

    public bool isKnocked()
    {
        return isBeingKnocked;
    }
}