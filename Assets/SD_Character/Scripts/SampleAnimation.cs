using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleAnimation : MonoBehaviour
{

    // Animator コンポーネント
    private Animator animator;
    public Rigidbody rb;
    public float forwardForce = 20f;

    public float xVel = 0;
    public float yVel = 0;
    public float zVel = 0;

    public int maxLife = 3;
    public float minSpeed = 10f;
    public float maxSpeed = 20f;
    public float invincibleTime;
    public GameObject model;

    // 設定したフラグの名前
    private const string key_isRun = "IsRun";
    private const string key_isAttack01 = "IsAttack01";
    private const string key_isAttack02 = "IsAttack02";
    private const string key_isJump = "IsJump";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";
    //private const string key_isTurnRight = "IsTurnRight";
    // private const string key_isTurnLeft = "IsTurnLeft";

    private int currentLife;
    private bool invincible = false;
    static int blinkingValue;

    private UIManager uiManager;
        
    // 初期化メソッド
    void Start()
    {
        // 自分に設定されているAnimatorコンポーネントを習得する
        this.animator = GetComponent<Animator>();
        currentLife = maxLife;
        forwardForce = minSpeed;
        blinkingValue = Shader.PropertyToID("_BlinkingValue");
        uiManager = FindObjectOfType<UIManager>();
    }

    // 1フレームに1回コールされる
    void Update()
    {
        // 矢印上ボタンを押下している
        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow)))
        {
            // IdleからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        else
        {
            // RunからIdleに遷移する
            this.animator.SetBool(key_isRun, false);
        }

        // パンチ aを押す
        if (Input.GetKey("a"))
        {
            TurnLeft();

        }
        else
        {
            // Attack01からIdleに遷移する
            this.animator.SetBool(key_isAttack01, false);
        }

        if (Input.GetKey("w"))
        {
            //Attack01に遷移する
            TurnForward();
        }

        // キック sを押す
        if (Input.GetKey("s"))
        {
            TurnBack();
            //Attack02に遷移する
        }
        else
        {
            // Attack02からIdleに遷移する
            this.animator.SetBool(key_isAttack02, false);
        }

        // ジャンプ spaceを押す
        if (Input.GetKey("space"))
        {
            //Jumpに遷移する
            this.animator.SetBool(key_isJump, true);
        }
        else
        {
            // JumpからIdleに遷移する
            this.animator.SetBool(key_isJump, false);
        }

        // ダメージ ｄを押す
        if (Input.GetKey("d"))
        {
            //Damageに遷移する
            TurnRight();
        }
        else
        {
            // DamageからIdleに遷移する
            this.animator.SetBool(key_isDamage, false);
        }

        // GetComponent<Rigidbody>().velocity = new Vector3(xVel, yVel, zVel);
    }
    //  IEnumerator stopRotation()
    // {
    //  yield return new WaitForSeconds(.1f);
    //  GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
    //  GetComponent<Transform>().eulerAngles = new Vector3(0, -30, 0);
    //  }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            this.animator.SetBool(key_isDead, true);
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        }

    }

    void Restart()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void TurnLeft()
    {
        //Attack01に遷移する
        //zVel = 0;
        //xVel = 0;
        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -2, 0);
        this.animator.SetBool(key_isRun, true);
        rb.AddForce(0, 0, 50 * Time.deltaTime);
        transform.Rotate(Vector3.up, -20 * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime);
        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        //StartCoroutine(stopRotation());
    }
    void TurnRight()
    {
        //Attack01に遷移する
        //zVel = 0;
        //xVel = 0;
        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -2, 0);
        this.animator.SetBool(key_isRun, true);
        rb.AddForce(0, 0, -50 * Time.deltaTime);
        transform.Rotate(Vector3.up, -20 * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime);
        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        GetComponent<Transform>().eulerAngles = new Vector3(0, 180, 0);
        //StartCoroutine(stopRotation());
    }
    void TurnBack()
    {
        //Attack01に遷移する
        //zVel = 0;
        //xVel = 0;
        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -2, 0);
        this.animator.SetBool(key_isRun, true);
        rb.AddForce(-50 * Time.deltaTime, 0, 0);
        transform.Rotate(Vector3.up, -20 * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime);
        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        GetComponent<Transform>().eulerAngles = new Vector3(0, -90, 0);
        //StartCoroutine(stopRotation());
    }
    void TurnForward()
    {
        //Attack01に遷移する
        //zVel = 0;
        //xVel = 0;
        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -2, 0);
        GetComponent<Transform>().eulerAngles = new Vector3(0, 90, 0);
        this.animator.SetBool(key_isRun, true);
        rb.AddForce(50 * Time.deltaTime, 0, 0);
        transform.Rotate(Vector3.up, -20 * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime);
        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        
        //StartCoroutine(stopRotation());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (invincible)
            return;
        if (other.CompareTag("Obstacle"))
        {
            currentLife--;
            uiManager.UpdateLives(currentLife);
            animator.SetTrigger("Hit");
            forwardForce = 0;
            if(currentLife <= 0)
            {
                //game over
            }
            else
            {
                StartCoroutine(Blinking(invincibleTime));
            }
        }
    }
    IEnumerator Blinking(float time)
    {
        invincible = true;
        float timer = 0;
        float currentBlink = 1f;
        float lastBlink = 0;
        float blinkPeriod = 0.1f;
        bool enabled = false;
        yield return new WaitForSeconds(1f);
        forwardForce = minSpeed;
        while(timer < time && invincible)
        {
            model.SetActive(enabled);
           // Shader.SetGlobalFloat(blinkingValue, currentBlink);
            yield return null;
            timer += Time.deltaTime;
            lastBlink += Time.deltaTime;
            if(blinkPeriod < lastBlink)
            {
                lastBlink = 0;
                currentBlink = 1f -currentBlink;
                enabled = !enabled;
            }
        }
        model.SetActive(true);
       // Shader.SetGlobalFloat(blinkingValue, 0);
        invincible = false;
    }
}