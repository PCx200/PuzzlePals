using EventBus;
using UnityEngine;

public class ReleaseHappiness : SuperPower
{
    [SerializeField] private float radius;
    [SerializeField] private Material happyMaterial;
    [SerializeField] private LayerMask sadObj;
    public override void UseSuperPower()
    {
        var hitObj = Physics.SphereCastAll(transform.position, radius, transform.forward, 0, sadObj);

        foreach (var obj in hitObj)
        { 
            obj.collider.gameObject.GetComponent<MeshRenderer>().material = happyMaterial;
            Debug.Log("Happiness has activated");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.pink;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
