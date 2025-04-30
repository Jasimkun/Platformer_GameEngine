using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSize = 1;
    public float moveSpeed = 5f;
    public GameObject bossHealthBar;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundlayer;
    public GameObject attackCollider;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded, isPhoenix, speedUp, jumpUp, isAttackable;

    public bool isAttack = false;
    private float attackDelay = 0.5f;

    float score;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
        attackCollider.SetActive(false);

        score = 1000f;
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-playerSize, playerSize, playerSize);
            pAni.SetBool("Run", true);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(playerSize, playerSize, playerSize);
            pAni.SetBool("Run", true);
        }
        else
        {
            pAni.SetBool("Run", false);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
        Debug.DrawRay(groundCheck.position, Vector3.down * 0.2f);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            pAni.SetTrigger("JumpAction");
        }
        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true;
            pAni.SetTrigger("Attack");
            StartCoroutine(Attack());
            Invoke("ResetIsAttack", attackDelay);
        }
        score -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Respawn":
                if (!isPhoenix)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Finish":
                HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, (int)score);
                collision.GetComponent<LevelObject>().MoveToNextLevel();
                break;
            case "Enemy":
                if (!isPhoenix)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Phoenix":
                StartCoroutine(Phoenix());
                Destroy(collision.gameObject);
                break;
            case "SpeedUp":
                StartCoroutine(SpeedUp());
                Destroy(collision.gameObject);
                break;
            case "JumpUp":
                StartCoroutine(JumpUp());
                Destroy(collision.gameObject);
                break;
            case "DeadZone":
                if (!isPhoenix)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Realdeadzone":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }
    private IEnumerator Attack()
    {
        attackCollider.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        attackCollider.SetActive(false);
        yield break;
    }

    private IEnumerator Phoenix()
    {
        isPhoenix = true;
        yield return new WaitForSeconds(3f);
        isPhoenix = false;
    }


    private IEnumerator SpeedUp()
    {
        speedUp = true;
        moveSpeed *= 2;
        yield return new WaitForSeconds(3f);
        moveSpeed /= 2;
        speedUp = false;
    }
    private IEnumerator JumpUp()
    {
        jumpUp = true;
        jumpForce *= 2;
        yield return new WaitForSeconds(3f);
        jumpForce /= 2;
        jumpUp = false;
    }
    void ResetIsAttack()
    {
        isAttack = false;
    }
}
