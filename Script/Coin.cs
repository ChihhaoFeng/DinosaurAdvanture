using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Canvas myCanvas;

    private void Awake()
    {
        myCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player") 
        {

            int Coin = PlayerPrefs.GetInt("GetCoin") + 1;
            PlayerPrefs.SetInt("GetCoin", Coin);
            myCanvas.coinupdate();
 
            Destroy(this.gameObject);
        }




    }


}
