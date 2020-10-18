using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    GameObject player;
    public float speed = 1;
    public int health = 10;
    public int playerKnockbackMultiplier = 2;
    public int enemyKnockbackMultiplier = 2;

    bool isBeingKnocked = false;
    float knockTimer = 0.0f;

    Vector3 move;
    Vector3 playerKnock;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.GetComponent<SpawnEnemy>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            //Die

        }

        if (isBeingKnocked)
        {
            if (knockTimer >= 0.5f)
            {
                isBeingKnocked = false;
                knockTimer = 0.0f;
            }
            knockTimer += Time.deltaTime;
            move = this.transform.position - player.transform.position;
            move.Normalize();

            this.transform.position += move * enemyKnockbackMultiplier * Time.deltaTime;
        }
        else
        {
            move = player.transform.position - this.transform.position;
            move.Normalize();

            this.transform.position += move * speed * Time.deltaTime;
        }

        //Knockback for the player
        if (player.GetComponent<movement>().isKnocked())
        {
            playerKnock = player.transform.position - this.transform.position;
            playerKnock.Normalize();

            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position += playerKnock * playerKnockbackMultiplier * Time.deltaTime;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == player.name)
        {
            GameEvents.current.EnemyAttack();
        }

        if(other.tag == "Attack Hitbox")
        {
            isBeingKnocked = true;
            health--;
        }
    }
}
