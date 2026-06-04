using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
//this class is the super power of MIDA
//it can throw ice cream balls in front
public class IceCreamBalls : SuperPower
{
    [SerializeField] private GameObject iceCreamBallPrefab;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private float cooldown;
    private float currentCooldown;
    public override void UseSuperPower()
    {
        ThrowIceCreamBalls();
    }

    void ThrowIceCreamBalls()
    {        
        if (currentCooldown < 0)
        {
            var ball = Instantiate(iceCreamBallPrefab, ballSpawnPoint.transform.position, Quaternion.identity);

            Rigidbody rb = ball.GetComponent<Rigidbody>();

            rb.AddForce(Vector3.up * 3, ForceMode.Impulse);
            rb.AddForce(Vector3.forward, ForceMode.Impulse);

            currentCooldown = cooldown;

            Debug.Log("Throw Ice Cream Ball");

            Destroy(ball, 2);
        }
    }
    void Update()
    {
        if (currentCooldown >= 0.0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }
}
