using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigMinigame : MonoBehaviour
{
    char whichLetterNext = 'Q';
    int progress = 0;
    int finished = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && whichLetterNext == 'Q')
        {
            whichLetterNext = 'E';
            progress += 1;
        }
        if (Input.GetKeyDown(KeyCode.E) && whichLetterNext == 'E')
        {
            whichLetterNext = 'Q';
            progress += 1;
        }

        if(progress >= finished)
        {
            progress = 0;
        }
    }
}
