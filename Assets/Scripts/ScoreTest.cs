using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTest : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Level_1;
    public TextMeshProUGUI Level_2;
    public TextMeshProUGUI Level_3;
    public TextMeshProUGUI Level_4;
    public TextMeshProUGUI Level_5;


    void Start()
    {
        Level_1.text = "LEVEL 1 : " + HighScore.Load(1).ToString();
        Level_2.text = "LEVEL 2 : " + HighScore.Load(2).ToString();
        Level_3.text = "LEVEL 3 : " + HighScore.Load(3).ToString();
        Level_4.text = "LEVEL 4 : " + HighScore.Load(4).ToString();
        Level_5.text = "LEVEL 5 : " + HighScore.Load(5).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
