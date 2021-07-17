using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonCollider : MonoBehaviour
{
    Player playerscript;

    // Start is called before the first frame update
    private void Awake()
    {
        playerscript = GetComponentInParent<Player>();        // get the Player script
        


    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground") {
            playerscript.canJump = true;
            playerscript.myAnim.SetBool("jump",false);

        }
        if (collision.tag == "DynamicAirPlatform") 
        {
            playerscript.canJump = true;
            playerscript.myAnim.SetBool("jump", false);

            playerscript.transform.parent = collision.transform;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerscript.transform.parent = null;
    }





}


