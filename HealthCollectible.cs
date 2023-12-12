
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    

    AudioSource audioSource;



    void OnTriggerEnter2D(Collider2D other)
    {


        move player = other.GetComponent<move>();

        if (player != null)
        {
            // gefur stig eiðir sjér 
            Debug.Log(rememberMe.stig );
            rememberMe.stig = rememberMe.stig + 1;
            Destroy(gameObject);
            // ef stig nógu mörg fer í lokasenu
            if (rememberMe.stig == 6) { SceneManager.LoadScene(2); }
        }

        // spilar hlóð
        player.PlaySound(collectedClip);




    }
}