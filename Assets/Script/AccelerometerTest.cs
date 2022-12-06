using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AccelerometerTest : MonoBehaviour
{
    
    

    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;
    public int damageSaved = 0;

    // Start is called before the first frame update
    void Start()
    {        
        timerIsRunning = true;
        Accelerometer.Instance.OnShake += ActionToRunWhenShakingDevice;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {

                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    private void OnDestroy(){
        Accelerometer.Instance.OnShake -= ActionToRunWhenShakingDevice;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void saveShakeDamage(){
        damageSaved += 1;
    }

    private void ActionToRunWhenShakingDevice(){
        if(timerIsRunning){
            saveShakeDamage();   
        }
    }

    private void ActionToRunWhenDoneFading(){
        Debug.Log("Shake Done!");
    }
}
