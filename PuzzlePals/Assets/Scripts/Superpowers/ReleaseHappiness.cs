using EventBus;
using UnityEngine;

public class ReleaseHappiness : SuperPower
{
    private PlayerController player;
    [SerializeField] private float radius;
    [SerializeField] private Material happyMaterial;
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
        player.currentMonster.Animator.PlaySuperPower("ReleaseHappiness");


        foreach (var obj in hitObj)
        { 
            obj.collider.gameObject.GetComponent<MeshRenderer>().material = happyMaterial;
            HappinessDetector.Instance.happyObjCount++;            
            Debug.Log($"{obj.collider.gameObject.name} turned happy");
        }
        if (HappinessDetector.Instance != null) HappinessDetector.Instance.CheckHappiness();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.pink;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
