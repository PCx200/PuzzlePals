using System;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileTarget : MonoBehaviour
{
    [SerializeField] private Animator animation;
    private void OnTriggerEnter(Collider other)
    {
        animation.SetTrigger("Open");
        Debug.Log("target hit");
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.buttonClick, transform.position);
    }
}
