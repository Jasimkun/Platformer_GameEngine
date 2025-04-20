using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float raycastDistance = 0.2f;
    public float traceDistance = 2f;
    public float invincibilityTime = 0.1f; // 공격 받은 후 무적 시간

    public int maxHealth = 5; // 보스 최대 체력 (5번 맞으면 죽음)
    public int currentHealth; // 현재 체력

    // 체력바 스프라이트 배열 (총 5개)
    public SpriteRenderer healthBarRenderer;
    public Sprite[] healthBarSprites = new Sprite[5]; // 체력 5~1에 해당하는 스프라이트들

    private Transform player;
    private bool isInvincible = false; // 무적 상태 여부
    private bool isWalking = false; // 걷는 상태인지 여부
    public Animator animator; // 애니메이터 참조 (걷기 애니메이션용)

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;

        // 참조가 없는 경우 컴포넌트 찾기
        if (animator == null)
            animator = GetComponent<Animator>();

        if (healthBarRenderer == null)
            healthBarRenderer = transform.Find("HealthBar").GetComponent<SpriteRenderer>();

        // 시작 시 최대 체력에 맞는 스프라이트 표시
        UpdateHealthBarSprite();
    }

    void Update()
    {
        MoveTowardsPlayer();
        UpdateAnimation();
    }

    // 플레이어를 향해 걸어가는 로직
    void MoveTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;

        // 추적 거리 밖이면 움직이지 않음
        if (direction.magnitude > traceDistance)
        {
            isWalking = false;
            return;
        }

        isWalking = true;
        Vector2 directionNormalized = direction.normalized;

        // 장애물 감지
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
            // 걸어가는 움직임 구현
            transform.Translate(directionNormalized * moveSpeed * Time.deltaTime);

            // 플레이어 방향에 따라 보스의 방향 전환
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

    // 애니메이션 상태 업데이트
    void UpdateAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
        }
    }

    // 현재 체력에 맞는 체력바 스프라이트 표시
    void UpdateHealthBarSprite()
    {
        if (healthBarRenderer != null && healthBarSprites.Length >= 5)
        {
            // 인덱스 계산 (체력이 5일 때 인덱스 0, 체력이 1일 때 인덱스 4)
            int spriteIndex = maxHealth - currentHealth;

            // 유효 범위 확인
            if (spriteIndex >= 0 && spriteIndex < healthBarSprites.Length)
            {
                healthBarRenderer.sprite = healthBarSprites[spriteIndex];
            }
        }
    }

    // 공격 받았을 때 호출될 메서드
    public void TakeDamage()
    {
        if (isInvincible)
            return;

        // 체력 감소
        currentHealth--;

        // 현재 체력에 맞는 스프라이트로 즉시 변경
        UpdateHealthBarSprite();

        // 무적 시간 적용
        StartCoroutine(InvincibilityFrames());

        // 피격 애니메이션 재생
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        // 체력이 0 이하면 사망
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 무적 시간 코루틴
    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    // 사망 처리
    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // 사망 관련 추가 로직
        // 예: 게임 오브젝트 비활성화, 파티클 효과 생성 등
        Destroy(gameObject, 1f); // 애니메이션 재생 후 파괴
    }

    // 플레이어의 공격 콜라이더와 충돌 감지
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackCollider"))
        {
            // 플레이어 공격 판정
            TakeDamage(); // 공격받을 때마다 체력 1 감소
        }
    }
}