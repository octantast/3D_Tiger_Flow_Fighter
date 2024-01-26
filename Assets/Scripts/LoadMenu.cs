using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
   
    public float timerforloading = 7.5f;

    private void Update()
    {
        if (timerforloading > 0)
        {
            timerforloading -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
