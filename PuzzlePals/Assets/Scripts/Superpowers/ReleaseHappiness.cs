using EventBus;
using UnityEngine;

public class ReleaseHappiness : SuperPower
{
    [SerializeField] private float radius;
    [SerializeField] private Material happyMaterial;
    [SerializeField] private LayerMask sadObj;
    public override void SuperPowerPressed()
    {
        Debug.Log($"happiness released");
        var hitObj = Physics.SphereCastAll(transform.position, radius, transform.forward, 0, sadObj);
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.happiness, transform.position);

        foreach (var obj in hitObj)
        { 
            obj.collider.gameObject.GetComponent<MeshRenderer>().material = happyMaterial;
            Debug.Log($"{obj.collider.gameObject.name} turned happy");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.pink;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
