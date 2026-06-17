using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{

    [SerializeField] private BoxCollider area;

    [SerializeField] private Level level;
    [SerializeField] private int nextScene;
 
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
            // OnLevelCompleted.Invoke();
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + area.center, area.size);
    }
}
 
