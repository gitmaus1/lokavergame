using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using TMPro;


public class Takki : MonoBehaviour
{
    public TextMeshProUGUI textin;

    public void Start()
    {





        // wanst e�a d�st
        if (rememberMe.stig == 6)
        {
            textin.text =  " �� wanst til hamingju ";
        }
        else
        {
            textin.text =  " d�st ";
        }
    }








    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Byrja()
    {
        rememberMe.stig = 0;//n�ll stilli stigin 

        SceneManager.LoadScene(1);
    }


}


