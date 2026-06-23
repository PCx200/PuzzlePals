using System.Collections;
using UnityEngine;

public class Teleportation : SuperPower
{
    [SerializeField] private Transform player;
    [SerializeField] private BedTeleport currentBed;
    [SerializeField] private ParticleSystem bedParticles;

    public override void SuperPowerPressed()
    {
        if (currentBed == null) return;
        bedParticles.Play();
        player.position = currentBed.LinkedBed.transform.position + Vector3.up;
        var rb = player.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bed, transform.position);

        Debug.Log("Teleported to: " + player.position);

        StartCoroutine(ResetKinematic(rb));
    }

    IEnumerator ResetKinematic(Rigidbody rb)
    {
        yield return new WaitForSeconds(0.1f);
        rb.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bed"))
        {
            currentBed = other.GetComponent<BedTeleport>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentBed = null;
    }
}
