using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                     // For UI
// we don't need to call (using UnityEngine.SceneManagement;)  becasue we use the function from FadeInOut, it already have SceneManagement


public class SelectSceneButtonScript : MonoBehaviour
{
    int clearedLevel;
    public Sprite ButtonSprite;
    Image imageB1, imageB2, imageB3;
    private void Awake()
    {
        imageB1 = GameObject.Find("Canvas/SafeAreaPanel/SelectPanelBG/Level1Button").GetComponent<Image>();
        imageB2 = GameObject.Find("Canvas/SafeAreaPanel/SelectPanelBG/Level2Button").GetComponent<Image>();
        imageB3 = GameObject.Find("Canvas/SafeAreaPanel/SelectPanelBG/Level3Button").GetComponent<Image>();

        clearedLevel =PlayerPrefs.GetInt("clearedLevel",0);

        if (clearedLevel == 0)
        {
            imageB1.sprite = ButtonSprite;
        }
        else if (clearedLevel == 1)
        {
            imageB1.sprite = ButtonSprite;
            imageB2.sprite = ButtonSprite;
        }
        else if (clearedLevel >= 2)
        {
            imageB1.sprite = ButtonSprite;
            imageB2.sprite = ButtonSprite;
            imageB3.sprite = ButtonSprite;
        }

    }

    public void EnterLevel1() 
    {
        //SceneManager.LoadScene("Level1");
        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
        FadeInOut.instance.SceneFadeInOut("Level1");
    }
    public void EnterLevel2()
    {
        if (clearedLevel > 0)
        {
            //SceneManager.LoadScene("Level2");
            BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
            myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
            FadeInOut.instance.SceneFadeInOut("Level2");
        }
        else {
            BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
            myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[1]);
        }


    }
    public void EnterLevel3()
    {
        if (clearedLevel > 1)
        {
            //SceneManager.LoadScene("Level3");
            BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
            myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
            FadeInOut.instance.SceneFadeInOut("Level3");
        }
        else {
            BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
            myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[1]);
        }

    }

    public void EnterMainMenu() 
    {
        //SceneManager.LoadScene("MainMenu");
        BGM myBGM = GameObject.Find("BGMcontroler").GetComponent<BGM>();
        myBGM.myAudioSource.PlayOneShot(myBGM.ButtonClip[0]);
        FadeInOut.instance.SceneFadeInOut("MainMenu");
    }



}
