using System.Collections;
using UnityEngine;

public class Cupcake : SuperPower
{
    private PlayerController player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject cupcake;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }
    public override void SuperPowerPressed()
    {
        if (spawnPoint.childCount == 0)
        {
            Instantiate(cupcake, spawnPoint.transform.position, Quaternion.identity, spawnPoint);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.createCupcake, transform.position);
            player.currentMonster.Animator.PlaySuperPower("CreateCupcake");

        }
        else Debug.Log("There already exists a cupcake");
    }
}
