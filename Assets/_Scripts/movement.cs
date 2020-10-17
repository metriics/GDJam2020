using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 curMoveVector = new Vector3();
    [SerializeField]
    public float speed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        curMoveVector = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            curMoveVector += new Vector3(0.0f, 1.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            curMoveVector += new Vector3(-1.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            curMoveVector += new Vector3(0.0f, -1.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            curMoveVector += new Vector3(1.0f, 0.0f);
        }

        controller.Move(curMoveVector * speed * Time.deltaTime);
    }
}
