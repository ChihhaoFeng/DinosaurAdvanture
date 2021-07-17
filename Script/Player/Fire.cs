using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    GameObject Player;
    Rigidbody2D myRigid;

    public float Firespeed;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        myRigid = GetComponent<Rigidbody2D>();

        if (Player.transform.localScale.x == 1.0f) {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            myRigid.AddForce(Vector2.right* Firespeed, ForceMode2D.Impulse);
        }
        if (Player.transform.localScale.x == -1.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            myRigid.AddForce(Vector2.left* Firespeed, ForceMode2D.Impulse);
        }


        Destroy(this.gameObject, 3.0f);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || collision.tag == "Medicine" || collision.tag == "Coin")
        {

        }
        else 
        {
            Destroy(this.gameObject);
        }

    }


}
