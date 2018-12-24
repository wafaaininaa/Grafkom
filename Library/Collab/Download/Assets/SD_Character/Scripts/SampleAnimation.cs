using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleAnimation : MonoBehaviour
{

    // Animator コンポーネント
    private Animator animator;
    public Rigidbody rb;
    public float forwardForce = 2000f;

    // 設定したフラグの名前
    private const string key_isRun = "IsRun";
    private const string key_isAttack01 = "IsAttack01";
    private const string key_isAttack02 = "IsAttack02";
    private const string key_isJump = "IsJump";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";
    //private const string key_isTurnRight = "IsTurnRight";
   // private const string key_isTurnLeft = "IsTurnLeft";
    // 初期化メソッド
    void Start()
    {
        // 自分に設定されているAnimatorコンポーネントを習得する
        this.animator = GetComponent<Animator>();
    }

    // 1フレームに1回コールされる
    void Update()
    {
        // 矢印上ボタンを押下している
        if (Input.GetKey(KeyCode.UpArrow)||(Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow)))
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
            //Attack01に遷移する
            this.animator.SetBool(key_isRun, true);
            rb.AddForce(0, 0, 50 * Time.deltaTime);
        }
        else
        {
            // Attack01からIdleに遷移する
            this.animator.SetBool(key_isAttack01, false);
        }
        if (Input.GetKey("w"))
        {
            //Attack01に遷移する
            this.animator.SetBool(key_isRun, true);
            rb.AddForce(50 * Time.deltaTime, 0, 0);
        }

        // キック sを押す
        if (Input.GetKey("s"))
        {
            //Attack02に遷移する
            this.animator.SetBool(key_isRun, true);
            rb.AddForce(-50 * Time.deltaTime, 0, 0);
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
            this.animator.SetBool(key_isRun, true);
            rb.AddForce(0, 0, -50 * Time.deltaTime);
        }
        else
        {
            // DamageからIdleに遷移する
            this.animator.SetBool(key_isDamage, false);
        }

        // 死亡 fを押す 
    }
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
}