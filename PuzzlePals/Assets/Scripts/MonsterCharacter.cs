using UnityEngine;

public class MonsterCharacter : MonoBehaviour
{
    public enum MonsterName { Mida, Copkac, Home, Jullia }

    [SerializeField]  private MonsterName monsterName;
    public MonsterName Name => monsterName;

    [SerializeField] private GameObject prefab;

    public GameObject Prefab => prefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
