using UnityEngine;

public class MonsterCharacter : MonoBehaviour
{
    public enum MonsterName { Mida, Copkac, Home, Jullia }

    [SerializeField]  private MonsterName monsterName;
    public MonsterName Name => monsterName;

    [SerializeField] private GameObject prefab;

    public GameObject Prefab => prefab;

    [SerializeField] private MonsterStatsSO stats;

    public MonsterStatsSO Stats => stats;

    [SerializeField] private SuperPower superPower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void UseSuperPower()
    {
        superPower.UseSuperPower();
    }
}
