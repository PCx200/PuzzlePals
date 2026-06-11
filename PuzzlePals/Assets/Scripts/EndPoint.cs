using System;
using UnityEngine;

public class EndPoint : MonoBehaviour
{

    [SerializeField] private BoxCollider area;

    [SerializeField] private Level level;

    public event Action OnLevelCompleted;

    private void OnValidate()
    {
        if (area == null)
        {
            area = GetComponent<BoxCollider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnLevelCompleted.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + area.center, area.size);
    }
}
 
