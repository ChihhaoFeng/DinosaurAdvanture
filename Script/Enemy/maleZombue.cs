using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maleZombue : MonoBehaviour
{
    public Vector3 targetposition;
    protected Vector3 originposition,turnpoint;
    public float zombiespeed;
    public GameObject attackCollider;
    public int enemylife;



    protected bool isfirsttimeidel, isAttacked, isalive;
    protected GameObject MyPLayer;
    protected BoxCollider2D MyCollider;
    protected SpriteRenderer mySR;
    [SerializeField]                   // if it is protected but want to show in the Inspector
    protected AudioClip[] MyaudioClip;
    protected AudioSource MyaudioSource;

    protected Animator myAnim;
    // Start is called before the first frame update
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        MyCollider = GetComponent<BoxCollider2D>();
        mySR = GetComponent<SpriteRenderer>();
        MyaudioSource = GetComponent<AudioSource>();
        originposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        isfirsttimeidel = true;
        isAttacked = false;
        MyPLayer = GameObject.Find("Player");
        isalive = true;


    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveAttack();
    }

    protected virtual void MoveAttack() 
    {
        if (isalive)
        {
            if (Vector3.Distance(MyPLayer.transform.position, transform.position) < 1.2f)
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

                if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("attack") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("await"))
                {
                    return;
                }
                MyaudioSource.PlayOneShot(MyaudioClip[1]);
                myAnim.SetTrigger("attack");
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



    protected IEnumerator Turnside(bool turn) 
    {
        yield return new WaitForSeconds(2.0f);
        if (turn) {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        if (!turn)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }

    public void SetAttackColliderOn() 
    {
        attackCollider.SetActive(true);
    }
    public void SetAttackColliderOff()
    {
        attackCollider.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack") {
            enemylife--;
            if (enemylife >= 1)
            {
                MyaudioSource.PlayOneShot(MyaudioClip[0]);
                myAnim.SetTrigger("hurt");

            }
            else {
                isalive = false;
                MyCollider.enabled = false;
                MyaudioSource.PlayOneShot(MyaudioClip[0]);
                myAnim.SetTrigger("dead");
                StartCoroutine("afterDie");
            } 
        }
    }

    IEnumerator afterDie()    // let the dead zombie faded
    {
        yield return new WaitForSeconds(1.0f);
        mySR.material.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, 0.7f);   // mySR.color.r can also be 1.0f, 1.0f means oroiginal color 
        yield return new WaitForSeconds(1.0f);
        mySR.material.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, 0.3f);
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }




}
