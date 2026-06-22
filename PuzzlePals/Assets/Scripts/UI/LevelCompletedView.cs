using TMPro;
using UnityEngine;

public class LevelCompletedView : MonoBehaviour
{
    private EndPoint endPoint;
    private Level level;

    [SerializeField] private GameObject panel;

    [SerializeField] private TextMeshProUGUI completionTimeText;
    [SerializeField] private TextMeshProUGUI BestTimeText;

    private void Awake()
    {
        level = FindFirstObjectByType<Level>();
        endPoint = FindFirstObjectByType<EndPoint>();
    }

    private void OnEnable()
    {
        endPoint.OnLevelCompleted += EnableLevelCompletedPanel;
    }

    private void OnDisable()
    {
        endPoint.OnLevelCompleted -= EnableLevelCompletedPanel;
    }

    private void EnableLevelCompletedPanel()
    {
        if (level == null)
        {
            Debug.LogWarning($"Level: {level} is null");
            return;
        }
        if (endPoint == null)
        {
            Debug.LogWarning($"EndPoint: {endPoint} is null");
            return;
        }

            Cursor.lockState = CursorLockMode.None;
        panel.SetActive(true);

        completionTimeText.text = $"Completion Time: {level.CompletionTime / 60:00}:{level.CompletionTime % 60:00}";
        BestTimeText.text = $"Best Time: {level.LevelData.bestCompletionTime / 60:00}:{level.LevelData.bestCompletionTime % 60:00}";
    }
}
