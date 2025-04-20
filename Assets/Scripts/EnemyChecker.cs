using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChecker : MonoBehaviour
{
    public float checkInterval = 1f; // ���¸� �� �� �������� Ȯ������ (��)

    private void Start()
    {
        InvokeRepeating(nameof(CheckEnemies), 0f, checkInterval);
    }

    void CheckEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Debug.Log("��� ���� ���ŵǾ����ϴ�. ���� ������ �̵��մϴ�.");
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        // ������ ���� �ƴ϶�� ���� ������ �̵�
        if (currentSceneIndex + 1 < totalScenes)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.Log("������ ���Դϴ�. �� �̻� �Ѿ ���� �����ϴ�.");
        }
    }
}
