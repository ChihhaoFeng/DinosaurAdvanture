using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{

    public static FadeInOut instance;
    public GameObject FadeInOutImage;
    public Animator MyAnim;


    private void Awake()
    {   

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }


    public void SceneFadeInOut(string Level) 
    {

        StartCoroutine(Fadeinout(Level));
    }
    IEnumerator Fadeinout(string Level) 
    {
        FadeInOutImage.SetActive(true);

        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(Level);
        MyAnim.Play("FadeOut");

        yield return new WaitForSecondsRealtime(1.0f);
        FadeInOutImage.SetActive(false);
        Time.timeScale = 1.0f;
    }


}
