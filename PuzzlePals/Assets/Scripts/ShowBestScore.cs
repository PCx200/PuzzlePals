using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShowBestScore : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private List<Image> stars;

    private void Start()
    {
        ShowScore();
    }
    void ShowScore()
    {
        var entry = LevelManager.Instance.SaveData.GetOrCreate(sceneName);

        ushort bestTime = entry.bestTime;

        bestTimeText.text = "Best Time " + $"{FormatTime(bestTime)}";

        for (int i = 0; i < entry.stars; i++)
        {
            stars[i].gameObject.SetActive(true);
        }
    }
    private string FormatTime(float time)
    {
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}
