using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieKing : MonoBehaviour
{
    public Vector3 targetposition;
    protected Vector3 originposition, turnpoint;
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
            if (Vector3.Distance(MyPLayer.transform.position, transform.position) < 10.0f)
            {
                if (MyPLayer.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-10.0f, 10.0f, 1.0f);
                    //Debug.Log("on left");
                }
                else
                {
                    transform.localScale = new Vector3(10.0f, 10.0f, 1.0f);
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

                    isAttacked = false;
                }


            }


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
        if (collision.tag == "PlayerAttack")
        {
            enemylife--;
            if (enemylife >= 1)
            {
                MyaudioSource.PlayOneShot(MyaudioClip[0]);
                myAnim.SetTrigger("hurt");

            }
            else
            {
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
