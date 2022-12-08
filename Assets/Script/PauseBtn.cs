using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBtn : MonoBehaviour
{
    [SerializeField] GameObject Pausemenu;

    //public GameObject Pausemenu;

    public void Pause()
    {
        Pausemenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Pausemenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Mainmenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
