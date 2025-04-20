using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyAndHealthManager : MonoBehaviour
{
    public Image healthBarImage;             // ü�¹� UI �̹���
    public Sprite[] healthBarSprites;       // ü�¹� ��������Ʈ (5��: index 0~4)

    private int totalEnemies = 5;           // �� �� �� (5)
    [SerializeField] private int enemiesLeft;                // ���� ����ִ� �� ��

    void Start()
    {
        enemiesLeft = totalEnemies;
        UpdateHealthBar(); // �ʱ� ü�¹� ����
    }

    // ���� ���� �� ȣ��Ǵ� �Լ�
    public void OnEnemyKilled()
    {
        enemiesLeft = Mathf.Max(0, enemiesLeft - 1); // �ּ� 0������ �پ��

        if (enemiesLeft > 0)
        {
            UpdateHealthBar();
        }
        else
        {
            Debug.Log("��� ���� �׾����ϴ�. ���� ������ �̵��մϴ�.");
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
            Debug.Log("�� �̻� �Ѿ ���� �����ϴ�.");
        }
    }
}