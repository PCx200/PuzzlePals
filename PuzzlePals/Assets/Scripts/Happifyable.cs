using UnityEngine;
[RequireComponent (typeof(Material))]
public class Happifyable : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Color color = Color.pink;

    private void Awake()
    {
        //material = GetComponent<Material>();
    }
    private void OnTriggerEnter(Collider other)
    {
        material.SetColor("_BaseColor", color);
        Debug.Log("Set Color to " + color);
    }
}
