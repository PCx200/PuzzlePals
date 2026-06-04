using UnityEngine;

public class MonsterCharacter : MonoBehaviour
{
    public enum MonsterName { Mida, Copkac, Home, Jullia }

    [SerializeField]  private MonsterName monsterName;
    public MonsterName Name => monsterName;

    [SerializeField] private GameObject prefab;

    public GameObject Prefab => prefab;

    [SerializeField] private SuperPower superPower;

    public void UseSuperPower()
    {
        superPower.UseSuperPower();
    }
}
