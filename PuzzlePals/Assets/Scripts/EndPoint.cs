using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace EventBus
{
    public class EndPoint : MonoBehaviour
    {
        [SerializeField] private GameObject nextLevelText;
        [SerializeField] private string nextLevelSceneName;
        [SerializeField] private float waitTime;

        private Vector3 center;
        private Vector3 size;
        private void Awake()
        {
            center = GetComponent<BoxCollider>().center;
            center = GetComponent<BoxCollider>().size;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                //StartCoroutine(LoadNextLevel());
                FinishedLevel finishedLevel = new FinishedLevel();
                EventBus.Publish(finishedLevel);
                Debug.Log("Level finished");
            }
        }
        IEnumerator LoadNextLevel()
        {
            nextLevelText.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(nextLevelSceneName);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.darkOliveGreen;
            Gizmos.DrawCube(center, size);
        }
    }
} 
