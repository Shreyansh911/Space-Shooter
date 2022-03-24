using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject _pauseMenu;


    private bool _isGameOver;


    private void Start()
    {
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); // Current Game Scene
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // MainMenu
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseMenu();
        }

            
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void PauseMenu()
    {
        
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        
    }

}
