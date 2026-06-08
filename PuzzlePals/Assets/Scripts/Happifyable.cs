using UnityEngine;
[RequireComponent (typeof(Material))]
public class Happifyable : MonoBehaviour
{
    [SerializeField] private Material sadMaterial;
    [SerializeField] private Material happyMaterial;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        meshRenderer.material = sadMaterial;
    }
    private void OnTriggerEnter(Collider other)
    {
        meshRenderer.material = happyMaterial;
        Debug.Log("Set material to sad");
    }
}
