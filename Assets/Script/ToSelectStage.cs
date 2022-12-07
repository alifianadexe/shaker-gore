using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Continue Scene to Select Stage

public class ToSelectStage : MonoBehaviour
{
    public void SelectStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
    }
}