using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    bool paused = false;
    private GameObject pauseMenu;

    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("joystick button 7"))
        {
            paused = togglePause();

            var button = pauseMenu.transform.Find("Menu");
            if (button != null)
            {
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }
        }

    }

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            return (true);
        }
    }

    public void Resume()
    {
        paused = togglePause();

    }

    public void Restart()
    {
        paused = togglePause();
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void BackToMenu()
    {
        paused = togglePause();
        SceneManager.LoadScene(0);
    }
}

