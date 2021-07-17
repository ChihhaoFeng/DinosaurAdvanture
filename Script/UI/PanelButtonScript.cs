using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PanelButtonScript : MonoBehaviour
{
    public GameObject SelectPanel, PauseButton, levelSelectButton, mainMenuButton, replayButton;

    private void Awake()
    {
        PlayerPrefs.SetInt("Playerlife",5);
        PlayerPrefs.SetInt("GetCoin",0);
    }
    public void MainMenuButton()
    {
        //SceneManager.LoadScene("MainMenu");
        //Time.timeScale = 1.0f;
        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
        FadeInOut.instance.SceneFadeInOut("MainMenu");
    }

    public void LevelSelectButton() 
    {
        //SceneManager.LoadScene("LevelSelect");
        //Time.timeScale = 1.0f;
        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }
    public void ReplayButton()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene(sceneName);
        //Time.timeScale = 1.0f;
        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
        FadeInOut.instance.SceneFadeInOut(sceneName);
    }










    // For delete data in the main menu

    public void DataDeleteButton() 
    {
        
        RectTransform DatadeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        DatadeleteImage.anchoredPosition = new Vector2(0f, -260f);

        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
    }
    public void DataDeleteOn()
    {
        PlayerPrefs.DeleteAll();

        IsFirstTimeCheck checkscript = GameObject.Find("IsFirstTimeCheck").GetComponent<IsFirstTimeCheck>();
        checkscript.FirstTimePlayState();

        RectTransform DatadeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        DatadeleteImage.anchoredPosition = new Vector2(0f, 1500f);
        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
    }
    public void DataDeleteOff()
    {
        RectTransform DatadeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        DatadeleteImage.anchoredPosition = new Vector2(0f, 1500f);
        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
    }



    // Open the select Panel in each Level

    public void SetSelectPanelOn() 
    {
        SelectPanel.SetActive(true);    // showing the select panel
        Time.timeScale = 0f;            // pause the game
    }
    public void SetSelectPanelOff()
    {
        SelectPanel.SetActive(false);    // showing the select panel
        Time.timeScale = 1.0f;            // pause the game
    }



    public void SetStopButtonOn()
    {
        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
        PauseButton.SetActive(true);
    }
    public void SetStopButtonOff()
    {
        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
        PauseButton.SetActive(false);
    }


    // Go to the LevelSelect scene from main menu
    public void MainMenuPlayButton() 
    {   /*
        GameObject MainmenuPlayer = GameObject.Find("MainMenuPlayer");      
        Animator MyAnim = MainmenuPlayer.GetComponent<Animator>();
        MyAnim.SetBool("Run",true)
        */
        //SceneManager.LoadScene("LevelSelect");

        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);

        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }




}
