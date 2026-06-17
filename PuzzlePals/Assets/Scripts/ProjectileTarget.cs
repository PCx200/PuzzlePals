using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileTarget : MonoBehaviour
{
    [SerializeField] private UnityEvent onPress;
    private void OnTriggerEnter(Collider other)
    {
        onPress.Invoke();
        Debug.Log("target hit");
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.buttonClick, transform.position);
    }
}
