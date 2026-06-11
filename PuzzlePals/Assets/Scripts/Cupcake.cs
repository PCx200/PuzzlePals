using System.Collections;
using UnityEngine;

public class Cupcake : SuperPower
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject cupcake;
    [SerializeField] private float cooldown;
    private bool spawned;
    public override void UseSuperPower()
    {
        if (!spawned)
        {
            Instantiate(cupcake, spawnPoint.transform.position, Quaternion.identity, spawnPoint);
            spawned = true;
            StartCoroutine(CoolDownCupcake());
        }
    }

    IEnumerator CoolDownCupcake()
    {
        yield return new WaitForSeconds(cooldown);
        spawned = false;
    }
}
