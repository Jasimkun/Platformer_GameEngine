using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNewScript : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb. gravityScale = 100;

    }

    // Update is called once per frame
    void Update()
    {
        rb.gravityScale -= 0.1f;
    }
}
