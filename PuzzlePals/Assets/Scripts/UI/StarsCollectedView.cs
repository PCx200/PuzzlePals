using TMPro;
using UnityEngine;

public class StarsCollectedView : MonoBehaviour
{
    private byte currentStars;
    private byte allStars;

    [SerializeField] private TextMeshProUGUI starsText;

    private void Start()
    {
        UpdateStars();
    }

    private void UpdateStars()
    {
        currentStars = LevelManager.Instance.StarsCollected;
        allStars = (byte)(LevelManager.Instance.AllLevels.Count * 3);

        starsText.text = $"{currentStars:00}/{allStars:00}";
    }

}
