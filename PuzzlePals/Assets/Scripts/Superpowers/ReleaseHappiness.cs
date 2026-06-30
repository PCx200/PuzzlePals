using EventBus;
using UnityEngine;

public class ReleaseHappiness : SuperPower
{
    private PlayerController player;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask sadObj;
    [Header ("ParticleSystem Effects")] 
    [SerializeField] private ParticleSystem circle;
    [SerializeField] private ParticleSystem particles;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    public override void SuperPowerPressed()
    {
        Debug.Log($"happiness released");
        circle.Play();
        particles.Play();
        var hitObj = Physics.SphereCastAll(transform.position, radius, transform.forward, 0, sadObj);
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.happiness, transform.position);
        player.currentMonster.Animator.SetTrigger("Happiness");

        foreach (var hit in hitObj)
        {
            var sadMonster = hit.collider.transform.Find("SadMonster");
            if (sadMonster == null) continue;

            var animator = sadMonster.GetComponent<Animator>();
            if (animator == null) continue;

            if (HappinessDetector.Instance != null &&
                !HappinessDetector.Instance.RegisterHappy(hit.collider.gameObject))
                continue;

            animator.SetTrigger("Happy");
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.happyMonster, transform.position);
            Debug.Log($"{hit.collider.gameObject.name} turned happy");
        }

        if (HappinessDetector.Instance != null) HappinessDetector.Instance.CheckHappiness();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.pink;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
