using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class RankPage : MonoBehaviour
{
    [SerializeField] Transform contentRoot;

    [SerializeField] GameObject rowPrefab;

    StageResultList allData;

    int StageIndex = 1;
    
    public void ButtonAction(int Index)
    {
        StageIndex = Index;
        RefreshRankList();
    }
    void Awake()
    {
        allData = StageResultSaver.LoadRank();
        RefreshRankList();
    }

    void RefreshRankList()
    {
        //기존의 모든 자식 오브젝트 삭제
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        //랭크 데이터 정렬
        var sortedData = allData.results.Where(r => r.stage == StageIndex).OrderByDescending(x => x.score).ToList();

        //랭크 데이터 생성
        for (int i = 0; i < sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankTest = row.GetComponentInChildren<TMP_Text>();
            rankTest.text = $"{i + 1}. {sortedData[i].playerName} - {sortedData[i].score}";
        }
    }
}
