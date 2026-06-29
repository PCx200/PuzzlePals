using System.Collections;
using UnityEngine;

//this class is the super power of MIDA
//it can throw ice cream balls in front
public class IceCreamBalls : SuperPower
{
    private PlayerController player;
    [SerializeField] private GameObject iceCreamBallPrefab;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private float cooldown;
    [SerializeField] private float ballDespawnTime;
    private float currentCooldown;

    [SerializeField] private float upwardForce;
    [SerializeField] private float throwForce;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    public override void SuperPowerPressed()
    {
        StartCoroutine(ThrowIceCreamBalls());
    }

    IEnumerator ThrowIceCreamBalls()
    {        
        if (currentCooldown < 0)
        {
            currentCooldown = cooldown;

            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.throwBall, transform.position);
            player.currentMonster.Animator.SetTrigger("Throw");

            yield return new WaitForSeconds(0.1f);
            var ball = Instantiate(iceCreamBallPrefab, ballSpawnPoint.transform.position, Quaternion.identity);

            Rigidbody rb = ball.GetComponent<Rigidbody>();

            rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);



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
