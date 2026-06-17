using System.Runtime.CompilerServices;
using UnityEngine;

public class InteractableView : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject activeCanvas;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (activeCanvas != null)
        {
            Vector3 dir = Camera.main.transform.position - canvas.transform.position;
            activeCanvas.transform.LookAt(dir);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           activeCanvas = Instantiate(canvas, transform.position + new Vector3(0, 0.5f, 0),Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(activeCanvas);
        }
    }
}
