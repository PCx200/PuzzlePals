using UnityEngine;

public class UITrigger : MonoBehaviour
{
    [SerializeField] private GameObject uiToAppear;

    private void OnTriggerEnter(Collider other)
    {
        uiToAppear.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        uiToAppear.SetActive(false);
    }
}
