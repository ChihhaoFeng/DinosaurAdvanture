using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// we don't need to call (using UnityEngine.SceneManagement;)  becasue we use the function from FadeInOut, it already have SceneManagement
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    public float mySpeed;
    public float JumpForce;
    public int Firetime;
    float face = 1;
    public GameObject attackCollider, firePreFeb;
    float fireDistance;
    public int playerhealth;

    Rigidbody2D myRigi;

    // Start is called before the first frame update
    [HideInInspector]                  // hiddening the public object
    public Animator myAnim;
    public AudioClip[] MyAudioClip;
    AudioSource MyAudiosource;
    Canvas myCanvas;

    [HideInInspector]
    public bool isJumpPress, canJump, isAttack,ishurt, canbeHurt,canfire;

    InputAction PlayerMove,PlayerJump,PlayerAttack,PlayerFire;

    private void Awake()
    {
        PlayerMove = GetComponent<PlayerInput>().currentActionMap["Move"];
        PlayerJump = GetComponent<PlayerInput>().currentActionMap["Jump"];
        PlayerAttack = GetComponent<PlayerInput>().currentActionMap["Attack"];
        PlayerFire = GetComponent<PlayerInput>().currentActionMap["Fire"];

        myAnim = GetComponent<Animator>();
        myRigi = GetComponent<Rigidbody2D>();
        MyAudiosource = GetComponent<AudioSource>();
        myCanvas = GameObject.Find("/Canvas").GetComponent<Canvas>();

        isJumpPress = false;
        canJump = true;
        isAttack = false;
        ishurt = false;
        canbeHurt = true;
        playerhealth = PlayerPrefs.GetInt("Playerlife");

        canfire = true;


    }
   

    // Update is called once per frame
    void Update()
    {

        if (PlayerJump.triggered && canJump == true && ishurt ==false) {
            isJumpPress = true;
            canJump = false;
            MyAudiosource.PlayOneShot(MyAudioClip[2]);
            myAnim.SetBool("jump",true);
        }
        if (PlayerAttack.triggered && ishurt == false) {

            myAnim.SetTrigger("attack");
            isAttack = true;
            canJump = false;
        }
        if (PlayerFire.triggered && ishurt == false) {
            if (canfire) 
            {
                MyAudiosource.PlayOneShot(MyAudioClip[1]);
                myAnim.SetTrigger("attackfire");
                myRigi.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                isAttack = true;
                canJump = false;
                canfire = false;
                StartCoroutine("CanFire");
            }
            

        }

    }
    
    private void FixedUpdate()
    {
        //float a = Input.GetAxisRaw("Horizontal");
        float a = PlayerMove.ReadValue<Vector2>().x;
        //float a = Input.GetAxis("Horizontal");

        //float b = Input.GetAxisRaw("Vertical");

        if (isAttack || ishurt == true) {
            a = 0;
        }

        if (a > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            face = 1;
        }
        else if (a < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            face = -1;
        }
        else
        {
            if (face >= 1)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        
        /*
        if (Mathf.Abs(a) > 0.1f && b == 0)
        {
            myAnim.SetFloat("Run", Mathf.Abs(a));
        }
        else if (Mathf.Abs(b) > 0.1f && Mathf.Abs(a) == 0)
        {
            myAnim.SetFloat("Run", Mathf.Abs(b));
        }
        else if (Mathf.Abs(b) > 0.1f && Mathf.Abs(a) > 0.1f)
        {

            myAnim.SetFloat("Run", Mathf.Abs(a));
        }
        else
        {
            myAnim.SetFloat("Run", 0);
        }
        

        float tmpx = myRigi.position.x + a * Time.fixedDeltaTime * mySpeed;
        float tmpy = myRigi.position.y + b * Time.fixedDeltaTime * mySpeed;


        myRigi.position = new Vector2(tmpx, tmpy);  // *Time.deltaTime   can slow down this update 
        */


        if (Mathf.Abs(a) > 0.1f)
        {
            myAnim.SetFloat("Run", Mathf.Abs(a));
        }
        else {
            myAnim.SetFloat("Run", 0);
        }

        if (isJumpPress) {
            myRigi.AddForce(Vector2.up*JumpForce, ForceMode2D.Impulse);
            isJumpPress = false;
        }
        if (!ishurt) {
            myRigi.velocity = new Vector2(a * mySpeed, myRigi.velocity.y);
        }
  

    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground") {
            canJump = true;
            myAnim.SetBool("jump", false);
        }
    }
    */
    private void OnCollisionEnter2D(Collision2D collision)     // Once the Player fall out of the screen
    {
        if (collision.collider.name == "BoundButton") {
            playerhealth = 0;
            PlayerPrefs.SetInt("Playerlife", playerhealth);
            myCanvas.lifeupdate();
            ishurt = true;                   //stop moving
            myAnim.SetBool("Dead", true);
            isAttack = true;                 //stop turning side
            myRigi.velocity = new Vector2(0f, 0f);
            FadeInOut.instance.SceneFadeInOut("LevelSelect");
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.tag == "enemy" && ishurt == false && canbeHurt == true) {

            MyAudiosource.PlayOneShot(MyAudioClip[3]);

            if (playerhealth > 1)
            {
                ishurt = true;
                canbeHurt = false;
                myAnim.SetTrigger("Hurt");
                playerhealth--;
                PlayerPrefs.SetInt("Playerlife", playerhealth);
                myCanvas.lifeupdate();

                if (transform.localScale.x == 1.0f)
                {
                    myRigi.velocity = new Vector2(-2.0f, 10.0f);
                }
                else if (transform.localScale.x == -1.0f)
                {
                    myRigi.velocity = new Vector2(2.0f, 10.0f);
                }

                StartCoroutine("SetIsHurtFalse");
            }
            else {
                playerhealth--;
                PlayerPrefs.SetInt("Playerlife", playerhealth);
                myCanvas.lifeupdate();
                ishurt = true;                   //stop moving
                myAnim.SetBool("Dead", true);
                isAttack = true;                 //stop turning side
                myRigi.velocity = new Vector2(0f,0f);
                PlayerPrefs.SetInt("Playerlife",5);
                FadeInOut.instance.SceneFadeInOut("LevelSelect");
            }


            
        }
        if (collision.tag == "Coin") 
        {
            MyAudiosource.PlayOneShot(MyAudioClip[4]);
        }
        else if (collision.tag == "Medicine")
        {
            MyAudiosource.PlayOneShot(MyAudioClip[5]);
        }









    }
    IEnumerator SetIsHurtFalse() {
        yield return new WaitForSeconds(0.5f);
        ishurt = false;
        yield return new WaitForSeconds(0.5f);
        canbeHurt = true;
    }
    IEnumerator CanFire() 
    {
        yield return new WaitForSeconds(Firetime);
        canfire = true; 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "enemy" && ishurt == false && canbeHurt == true)
        {
            MyAudiosource.PlayOneShot(MyAudioClip[3]);


            if (playerhealth > 1)
            {

                ishurt = true;
                canbeHurt = false;
                myAnim.SetTrigger("Hurt");
                playerhealth--;
                PlayerPrefs.SetInt("Playerlife", playerhealth);
                myCanvas.lifeupdate();

                if (transform.localScale.x == 1.0f)
                {
                    myRigi.velocity = new Vector2(-2.0f, 10.0f);
                }
                else if (transform.localScale.x == -1.0f)
                {
                    myRigi.velocity = new Vector2(2.0f, 10.0f);
                }

                StartCoroutine("SetIsHurtFalse");
            }
            else
            {
                
                playerhealth--;
                PlayerPrefs.SetInt("Playerlife", playerhealth);
                myCanvas.lifeupdate();
                ishurt = true;
                myAnim.SetBool("Dead", true);
                isAttack = true;
                myRigi.velocity = new Vector2(0f, 0f);
                PlayerPrefs.SetInt("Playerlife", 5);
                FadeInOut.instance.SceneFadeInOut("LevelSelect");
            }


            
        }
    }

    public void SetIsAttackFalse() {
        isAttack = false;
        canJump = true;
        myAnim.ResetTrigger("attack");
        myAnim.ResetTrigger("attackfire");
    }
    public void Attackeffect() {
        MyAudiosource.PlayOneShot(MyAudioClip[0]);
    }


    public void ForIsHurtSetting() {
        isAttack = false;
        myAnim.ResetTrigger("attack");
        myAnim.ResetTrigger("attackfire"); 
        attackCollider.SetActive(false);
    } 
    
    public void SetAttackColliderOn() {
        attackCollider.SetActive(true);
    }
    public void SetAttackColliderOff() {
        attackCollider.SetActive(false);
    }
    public void fireInstantiate() {

        if (transform.localScale.x == 1.0f)
        {
            fireDistance = 2.0f;
        }
        if (transform.localScale.x == -1.0f)
        {
            fireDistance = -2.0f;
        }

        Vector3 temp = new Vector3(transform.position.x + fireDistance, transform.position.y, transform.position.z);
        Instantiate(firePreFeb, temp, Quaternion.identity);

    } 


}





// order of how Unity programming                    
//Awake -> OnEnable -> Start  ->                    //execute once    
// FixedUpdate -> OnTriggerEnter -> OncollisionEnter->
// Update -> LateUpdate ->
// OnDisable  -> OnDestory                          //execute once  