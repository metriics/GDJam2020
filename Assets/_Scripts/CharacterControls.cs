using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public float speed = 10.0f;

    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            move = new Vector3(move.x, 1.0f);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            move = new Vector3(move.x, 0.0f);
        }
        
        if(Input.GetKey(KeyCode.S))
        {
            move = new Vector3(move.x, -1.0f);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            move = new Vector3(move.x, 0.0f);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            move = new Vector3(1.0f, move.y);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            move = new Vector3(0.0f, move.y);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            move = new Vector3(-1.0f, move.y);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            move = new Vector3(0.0f, move.y);
        }

        //move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        this.transform.position += move * speed * Time.deltaTime;
    }
}
