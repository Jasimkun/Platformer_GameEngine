using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChecker : MonoBehaviour
{
    public float checkInterval = 1f; // 상태를 몇 초 간격으로 확인할지 (초)

    private void Start()
    {
        InvokeRepeating(nameof(CheckEnemies), 0f, checkInterval);
    }

    void CheckEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Debug.Log("모든 적이 제거되었습니다. 다음 씬으로 이동합니다.");
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        // 마지막 씬이 아니라면 다음 씬으로 이동
        if (currentSceneIndex + 1 < totalScenes)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.Log("마지막 씬입니다. 더 이상 넘어갈 씬이 없습니다.");
        }
    }
}
