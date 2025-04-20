using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float raycastDistance = 0.2f;
    public float traceDistance = 2f;
    public float invincibilityTime = 0.1f; // ���� ���� �� ���� �ð�

    public int maxHealth = 5; // ���� �ִ� ü�� (5�� ������ ����)
    public int currentHealth; // ���� ü��

    // ü�¹� ��������Ʈ �迭 (�� 5��)
    public SpriteRenderer healthBarRenderer;
    public Sprite[] healthBarSprites = new Sprite[5]; // ü�� 5~1�� �ش��ϴ� ��������Ʈ��

    private Transform player;
    private bool isInvincible = false; // ���� ���� ����
    private bool isWalking = false; // �ȴ� �������� ����
    public Animator animator; // �ִϸ����� ���� (�ȱ� �ִϸ��̼ǿ�)

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;

        // ������ ���� ��� ������Ʈ ã��
        if (animator == null)
            animator = GetComponent<Animator>();

        if (healthBarRenderer == null)
            healthBarRenderer = transform.Find("HealthBar").GetComponent<SpriteRenderer>();

        // ���� �� �ִ� ü�¿� �´� ��������Ʈ ǥ��
        UpdateHealthBarSprite();
    }

    void Update()
    {
        MoveTowardsPlayer();
        UpdateAnimation();
    }

    // �÷��̾ ���� �ɾ�� ����
    void MoveTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;

        // ���� �Ÿ� ���̸� �������� ����
        if (direction.magnitude > traceDistance)
        {
            isWalking = false;
            return;
        }

        isWalking = true;
        Vector2 directionNormalized = direction.normalized;

        // ��ֹ� ����
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionNormalized, raycastDistance);
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        bool hitObstacle = false;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
            {
                hitObstacle = true;
                Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * direction.normalized;
                transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime);
                break;
            }
        }

        if (!hitObstacle)
        {
            // �ɾ�� ������ ����
            transform.Translate(directionNormalized * moveSpeed * Time.deltaTime);

            // �÷��̾� ���⿡ ���� ������ ���� ��ȯ
            Vector3 scale = transform.localScale;
            float xScale = Mathf.Abs(scale.x);

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

    // �ִϸ��̼� ���� ������Ʈ
    void UpdateAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
        }
    }

    // ���� ü�¿� �´� ü�¹� ��������Ʈ ǥ��
    void UpdateHealthBarSprite()
    {
        if (healthBarRenderer != null && healthBarSprites.Length >= 5)
        {
            // �ε��� ��� (ü���� 5�� �� �ε��� 0, ü���� 1�� �� �ε��� 4)
            int spriteIndex = maxHealth - currentHealth;

            // ��ȿ ���� Ȯ��
            if (spriteIndex >= 0 && spriteIndex < healthBarSprites.Length)
            {
                healthBarRenderer.sprite = healthBarSprites[spriteIndex];
            }
        }
    }

    // ���� �޾��� �� ȣ��� �޼���
    public void TakeDamage()
    {
        if (isInvincible)
            return;

        // ü�� ����
        currentHealth--;

        // ���� ü�¿� �´� ��������Ʈ�� ��� ����
        UpdateHealthBarSprite();

        // ���� �ð� ����
        StartCoroutine(InvincibilityFrames());

        // �ǰ� �ִϸ��̼� ���
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        // ü���� 0 ���ϸ� ���
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ���� �ð� �ڷ�ƾ
    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    // ��� ó��
    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // ��� ���� �߰� ����
        // ��: ���� ������Ʈ ��Ȱ��ȭ, ��ƼŬ ȿ�� ���� ��
        Destroy(gameObject, 1f); // �ִϸ��̼� ��� �� �ı�
    }

    // �÷��̾��� ���� �ݶ��̴��� �浹 ����
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackCollider"))
        {
            // �÷��̾� ���� ����
            TakeDamage(); // ���ݹ��� ������ ü�� 1 ����
        }
    }
}