using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public Text lifeText,coinText;

    private void Awake()
    {
        lifeupdate();
        coinupdate();



    }
    public void lifeupdate() 
    {
        lifeText.text = "X" + PlayerPrefs.GetInt("Playerlife").ToString();
    }
    public void coinupdate() 
    {
        coinText.text = "X" + PlayerPrefs.GetInt("GetCoin").ToString();
    }



}
