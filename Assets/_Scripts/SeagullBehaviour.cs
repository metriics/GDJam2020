using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullBehaviour : MonoBehaviour
{
    GameObject player;
    public float speed = 3;
    public float diveSpeed = 1;
    public int health = 1;
    public Vector3 max;
    public Vector3 min;

    Vector3 diveTarget;
    Vector3 initialPos;
    float diveTimer = 0.0f;
    bool isDiving = false;
    bool isRising = false;
    bool idle = true;
    bool isFacingRight = false;

    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.GetComponent<SpawnSeagull>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if (idle)
        {
            diveTimer += Time.deltaTime;
            if(this.transform.position.x >= max.x)
            {
                isFacingRight = false;
            }
            else if(this.transform.position.x <= min.x)
            {
                isFacingRight = true;
            }

            if (isFacingRight)
            {
                this.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                move = new Vector3(1.0f, 0.0f, 0.0f);
            }
            else
            {
                this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                move = new Vector3(-1.0f, 0.0f, 0.0f);
            }

            this.transform.position += move * speed * Time.deltaTime;
        }

        if(diveTimer >= 5.0f && !isDiving && !isRising)
        {
            isDiving = true;
            idle = false;
            diveTarget = player.transform.position;
            float tempX = diveTarget.x - this.transform.position.x;
            initialPos = this.transform.position;
            initialPos.x += tempX * 2;
        }

        if (isDiving)
        {
            if (this.transform.position.y <= diveTarget.y)
            {
                isDiving = false;
                isRising = true;
            }
            else
            {
                move = diveTarget - this.transform.position;
                move.Normalize();
                float angle = Mathf.Atan2(move.y, move.x);

                if(move.x < 0.0f)
                {
                    isFacingRight = false;
                    this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle * Mathf.Rad2Deg + 180.0f);
                }
                else
                {
                    isFacingRight = true;
                    this.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, angle * Mathf.Rad2Deg + 180.0f);
                }

                this.transform.position += move * speed * diveSpeed * Time.deltaTime;
            }
        }

        if (isRising)
        {
            if (this.transform.position.y >= initialPos.y)
            {
                isRising = false;
                idle = true;
                diveTimer = 0.0f;
            }
            else
            {
                move = initialPos - this.transform.position;
                move.Normalize();
                float angle = Mathf.Atan2(move.y, move.x);
                this.transform.localRotation = Quaternion.Euler(0.0f, this.transform.localRotation.y, angle * Mathf.Rad2Deg + 180.0f);
                this.transform.position += move * speed * diveSpeed * Time.deltaTime;
            }
        }

        if (health <= 0)
        {
            //Die
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == player.name)
        {
            GameEvents.current.EnemyAttack();
        }

        if (other.tag == "Attack Hitbox")
        {
            health--;
        }
    }
}
