using UnityEngine;

//this class is the super power of MIDA
//it can throw ice cream balls in front
public class IceCreamBalls : SuperPower
{
    [SerializeField] private GameObject iceCreamBallPrefab;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private float cooldown;
    [SerializeField] private float ballDespawnTime;
    private float currentCooldown;

    [SerializeField] private float upwardForce;
    [SerializeField] private float throwForce;

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

            rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
            rb.AddForce(Vector3.forward * throwForce, ForceMode.Impulse);

            currentCooldown = cooldown;

            Destroy(ball, ballDespawnTime);
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
