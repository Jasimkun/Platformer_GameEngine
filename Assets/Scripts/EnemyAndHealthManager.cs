using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyAndHealthManager : MonoBehaviour
{
    public Image healthBarImage;             // 체력바 UI 이미지
    public Sprite[] healthBarSprites;       // 체력바 스프라이트 (5개: index 0~4)

    private int totalEnemies = 5;           // 총 적 수 (5)
    [SerializeField] private int enemiesLeft;                // 현재 살아있는 적 수

    void Start()
    {
        enemiesLeft = totalEnemies;
        UpdateHealthBar(); // 초기 체력바 설정
    }

    // 적이 죽을 때 호출되는 함수
    public void OnEnemyKilled()
    {
        enemiesLeft = Mathf.Max(0, enemiesLeft - 1); // 최소 0까지만 줄어듦

        if (enemiesLeft > 0)
        {
            UpdateHealthBar();
        }
        else
        {
            Debug.Log("모든 적이 죽었습니다. 다음 씬으로 이동합니다.");
            LoadNextScene();
        }
    }

    void UpdateHealthBar()
    {
        int spriteIndex = Mathf.Clamp(enemiesLeft - 1, 0, healthBarSprites.Length - 1);
        healthBarImage.sprite = healthBarSprites[spriteIndex];
    }

    void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        if (currentScene + 1 < totalScenes)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            Debug.Log("더 이상 넘어갈 씬이 없습니다.");
        }
    }
}