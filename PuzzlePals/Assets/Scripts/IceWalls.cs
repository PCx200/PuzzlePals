using UnityEngine;

public class IceWalls : MonoBehaviour
{
    [SerializeField] private Material transparentMaterial;
    [SerializeField] private Material opaqueMaterial;
    [SerializeField] private GameObject[] iceCreamwalls;
    public static IceWalls instance;

    private void Awake()
    {
        instance = this;
        TurnOpaque();
    }

    public void TurnTransparent()
    {
        foreach (var wall in iceCreamwalls)
        {
            wall.GetComponent<MeshRenderer>().material = transparentMaterial;
        }
    }
    public void TurnOpaque()
    {
        foreach (var wall in iceCreamwalls)
        {
            wall.GetComponent<MeshRenderer>().material = opaqueMaterial;
        }
    }
}
