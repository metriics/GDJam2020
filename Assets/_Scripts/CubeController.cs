using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onTouchCube += OnCubeColourChange;
    }

    private void OnCubeColourChange()
    {
        Color randColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        GetComponent<MeshRenderer>().material.SetColor("rand", randColor);
    }
}
