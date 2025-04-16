using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSize = 1;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundlayer;
    
    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded, isPhoenix, speedUp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent <Animator>();
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
            pAni.SetTrigger("JumpAction");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn") && !isPhoenix)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }
        if (collision.CompareTag("Enemy") && !isPhoenix)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.CompareTag("Phoenix"))
        {
            StartCoroutine(Phoenix());
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedUp());
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Realdeadzone"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
}
