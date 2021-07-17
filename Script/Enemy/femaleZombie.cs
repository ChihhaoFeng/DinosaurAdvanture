using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class femaleZombie : maleZombue
{
    // Start is called before the first frame update
    
    protected override void MoveAttack() 
    {

        if (isalive)
        {
            if (!((transform.position.x >= targetposition.x && transform.position.x >= originposition.x) || (transform.position.x <= targetposition.x && transform.position.x <= originposition.x)))
            {

                if (Vector3.Distance(MyPLayer.transform.position, transform.position) < 4.0f)
                {
                    
                    if (MyPLayer.transform.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                        //Debug.Log("on left");
                    }
                    else
                    {
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        //Debug.Log("on right");
                    }
                    Vector3 newtarget = new Vector3(MyPLayer.transform.position.x, transform.position.y, transform.position.z);
                    if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                    {
                        transform.position = Vector3.MoveTowards(transform.position, newtarget, zombiespeed * 2.0f * Time.deltaTime);
                    }




                    isAttacked = true;
                    return;                            // won't go throw rest of code




                }
                else
                {
                    if (isAttacked)                       //after attack and the player leave the zombie
                    {
                        if (turnpoint == targetposition)
                        {
                            if (targetposition.x > originposition.x)
                            {
                                StartCoroutine(Turnside(true));
                            }
                            else
                            {
                                StartCoroutine(Turnside(false));
                            }

                        }
                        else if (turnpoint == originposition)
                        {
                            if (targetposition.x > originposition.x)
                            {
                                StartCoroutine(Turnside(false));
                            }
                            else
                            {
                                StartCoroutine(Turnside(true));
                            }
                        }

                        isAttacked = false;
                    }


                }



            }



            if (transform.position.x == targetposition.x)
            {
                myAnim.SetTrigger("idel");
                turnpoint = originposition;

                if (targetposition.x > originposition.x) { StartCoroutine(Turnside(false)); }
                else if (targetposition.x < originposition.x) { StartCoroutine(Turnside(true)); }
                isfirsttimeidel = false;
            }
            else if (transform.position.x == originposition.x)
            {
                if (!isfirsttimeidel)
                {
                    myAnim.SetTrigger("idel");
                }

                turnpoint = targetposition;
                if (targetposition.x > originposition.x) { StartCoroutine(Turnside(true)); }
                else if (targetposition.x < originposition.x) { StartCoroutine(Turnside(false)); }


            }

            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            {
                transform.position = Vector3.MoveTowards(transform.position, turnpoint, zombiespeed * Time.deltaTime);
            }

        }


    }


    
}
