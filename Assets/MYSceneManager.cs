using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MYSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("SampleScene_Door1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
