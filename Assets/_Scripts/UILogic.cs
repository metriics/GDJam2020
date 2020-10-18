using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
            pauseMenu.SetActive(isActive);
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
