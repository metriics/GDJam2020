using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public float speed = 1;

    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = player.transform.position - this.transform.position;
        move.Normalize();

        this.transform.position += move * speed * Time.deltaTime;
    }
}
