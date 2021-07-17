using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creation : MonoBehaviour
{

    public GameObject FM_Zombie;
    bool Iscreate;
    private void Awake()
    {
        Iscreate = true;
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            if (Iscreate) 
            {
                Iscreate = false;
                StartCoroutine("CreateZ");
            }
        
        
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Iscreate)
            {
                Iscreate = false;
                StartCoroutine("CreateZ");
            }


        }
    }
    IEnumerator CreateZ() 
    {
        Vector3 temp = new Vector3(40.42f, 2.82f, 0f);
        Instantiate(FM_Zombie, temp, Quaternion.identity);
        yield return new WaitForSeconds(5.0f);
        Iscreate = true;

    }

}
