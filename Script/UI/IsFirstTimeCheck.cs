using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFirstTimeCheck : MonoBehaviour
{
    private void Awake()
    {
        FirstTimePlayState();

    }
    public void FirstTimePlayState() 
    {
        if (!PlayerPrefs.HasKey("IsFirstPlay"))
        {
            PlayerPrefs.SetInt("IsFirstTimePlay", 1);

            PlayerPrefs.SetInt("Playerlife", 5);

            PlayerPrefs.SetInt("GetCoin", 0);
        }
    }


}
