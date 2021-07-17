using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    Player myPlayer;
    Canvas myCanvas;


    private void Awake()
    {
        myPlayer = GameObject.Find("Player").GetComponent<Player>();
        myCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();

    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player") 
        {
            int life = PlayerPrefs.GetInt("Playerlife") + 1;
            PlayerPrefs.SetInt("Playerlife", life);
            myPlayer.playerhealth = life;
            myCanvas.lifeupdate();
            Destroy(this.gameObject);
        }
    }







}
