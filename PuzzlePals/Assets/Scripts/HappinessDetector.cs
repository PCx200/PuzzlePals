using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Unity.VisualScripting;

public class HappinessDetector : MonoBehaviour
{
    [SerializeField] private LayerMask sadObjectLayer;
    [SerializeField] private Material happyMaterial;
    [SerializeField] private UnityEvent onAllHappy;
    [SerializeField] private List<GameObject> sadObjects;
    public int happyObjCount;

    public static HappinessDetector Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void CheckHappiness()
    {
        if(happyObjCount == sadObjects.Count)
        {
            onAllHappy.Invoke();
            Debug.Log("All objects are happy");
        }
    }

}
