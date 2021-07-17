using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public AudioClip[] MyAudioClip;
    AudioSource MyAudiosource;

    private void Awake()
    {
        MyAudiosource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.name == "Player") 
        {

            int coin = PlayerPrefs.GetInt("GetCoin");
            if (coin >= 3)
            {
                string LevelName = SceneManager.GetActiveScene().name;       // get the number of the active scene
                string LevelNumber = LevelName.Substring(5);                // Eventually, we can use the same WinTrigger script in all Levels
                int Number = int.Parse(LevelNumber);

                int ClearedLevel = PlayerPrefs.GetInt("clearedLevel");

                if (Number > ClearedLevel)
                {

                    PlayerPrefs.SetInt("clearedLevel", Number);          // An apporach to store data wmong different scenes
                }



                MyAudiosource.PlayOneShot(MyAudioClip[0]);
                StartCoroutine("Delaytransport");
            }
            else 
            {
                MyAudiosource.PlayOneShot(MyAudioClip[1]);
            }


        }
    }


    IEnumerator Delaytransport() 
    {
        yield return new WaitForSeconds(1.0f);
        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }




}
