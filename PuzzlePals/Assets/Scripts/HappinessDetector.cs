using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class HappinessDetector : MonoBehaviour
{
    [SerializeField] private UnityEvent onAllHappy;
    [SerializeField] private List<GameObject> sadObjects;

    public static HappinessDetector Instance;

    private readonly HashSet<GameObject> happyObjects = new();

    private void Awake()
    {
        Instance = this;
    }

    public bool RegisterHappy(GameObject sadObject)
    {
        if (!sadObjects.Contains(sadObject) || happyObjects.Contains(sadObject))
            return false;

        happyObjects.Add(sadObject);
        return true;
    }

    public void CheckHappiness()
    {
        if (sadObjects.Count == 0) return;

        if (happyObjects.Count == sadObjects.Count)
        {
            onAllHappy.Invoke();
            Debug.Log("All objects are happy");
        }
    }
}
