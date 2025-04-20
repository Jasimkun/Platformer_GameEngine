using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{

    public float moveSpeed = 0.5f;
    public float raycastDistance = 0.2f;
    public float traceDistance = 2f;
    private EnemyAndHealthManager manager;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        manager = FindObjectOfType<EnemyAndHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 direction = player.position - transform.position;

        if (direction.magnitude > traceDistance) 
            return;

        Vector2 directionNormalized = direction.normalized;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionNormalized, raycastDistance);
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        foreach(RaycastHit2D rHit in hits)
        {
            if (rHit.collider != null && rHit.collider.CompareTag("Obstacle"))
            {
                Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * direction;
                transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime);
            }
            else{
                transform.Translate(direction * moveSpeed * Time.deltaTime);

                Vector3 scale = transform.localScale;
                float xScale = Mathf.Abs(transform.localScale.x);

                
                
                if (direction.x < 0)
                {
                    scale.x = xScale;
                    transform.localScale = scale;
                }
                else
                {
                    scale.x = -xScale;
                    transform.localScale = scale;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision other)
    {
        if(other.gameObject.CompareTag("Attack"))
        {
            manager.OnEnemyKilled();
            Destroy(this.gameObject);
        }
    }
}
