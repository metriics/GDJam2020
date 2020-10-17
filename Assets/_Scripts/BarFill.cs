using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarFill : MonoBehaviour
{
    Vector3 localScale;
    public float max = 0.96f;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = (DigMinigame.GetProgress() / 100.0f) * max;
        transform.localScale = localScale;
    }
}
