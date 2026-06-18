using System.Collections;
using UnityEngine;

public class Cupcake : SuperPower
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject cupcake;
    public override void SuperPowerPressed()
    {
        if (spawnPoint.childCount == 0)
        {
            Instantiate(cupcake, spawnPoint.transform.position, Quaternion.identity, spawnPoint);
        }
        else Debug.Log("There already exists a cupcake");
    }
}
