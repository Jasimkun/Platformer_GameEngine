using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveScript : MonoBehaviour
{
    public Animator myAnimator; // root에 있는거 드래그 앤 드롭 하기
    public int speed = 5;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        if(direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            myAnimator.SetBool("move", true);
        }
        else if(direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            myAnimator.SetBool("move", true);
        }
        else
        { 
            myAnimator.SetBool("move", false); 
        }
            

        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(Vector2.up * 300);

    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene_" +  collision.name);
    }
}
