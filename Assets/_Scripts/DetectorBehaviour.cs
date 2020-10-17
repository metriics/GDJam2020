using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorBehaviour : MonoBehaviour
{
    public Sprite on;
    public Sprite off;
    public float blinkDelay = 0.25f;

    private float blinkTimer = 0.0f;
    public Collider warmCollider;
    private float colliderRadius;
    private string status = "off";
    private bool recentStatusChange = false;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onColdLoot += SetStatusOff;
        GameEvents.current.onWarmLoot += SetStatusBlinking;

        colliderRadius = warmCollider.GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (recentStatusChange)
        {
            if (status == "off")
            {
                GetComponent<SpriteRenderer>().sprite = off;
            }
            else if (status == "blink")
            {
                GetComponent<SpriteRenderer>().sprite = on;
                blinkTimer = 0.0f;
            }
            else if (status == "on")
            {
                GetComponent<SpriteRenderer>().sprite = on;
            }

            recentStatusChange = false;
        }

        if (status == "blink")
        {
            blinkTimer += Time.deltaTime;
            if (blinkTimer < blinkDelay)
            {
                GetComponent<SpriteRenderer>().sprite = on;
            }
            else if (blinkTimer > blinkDelay && blinkTimer < blinkDelay * 2)
            {
                GetComponent<SpriteRenderer>().sprite = off;
            }
            else
            {
                blinkTimer = 0.0f;
            }


            // ---- why doesnt this work???
            // check if close to loot, only include colliders on layer 9 (Loot layer)
            //Collider[] hits = Physics.OverlapSphere(warmCollider.transform.position, colliderRadius/3, 9);
            //if (hits.Length > 0)
            //{
            //    SetStatusOn();
            //}
        }
    }

    private void SetStatusOff()
    {
        Debug.Log("Cold");
        status = "off";
        recentStatusChange = true;
    }

    private void SetStatusBlinking()
    {
        Debug.Log("Warm");
        status = "blink";
        recentStatusChange = true;
    }

    private void SetStatusOn()
    {
        Debug.Log("Hot");
        status = "on";
        recentStatusChange = true;
    }
}
