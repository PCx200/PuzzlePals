using UnityEngine;
using System.Collections.Generic;

public class MonsterCharacter : MonoBehaviour
{
    public enum MonsterName { Mida, Copkac, Home, Jullia }

    [SerializeField]  private MonsterName monsterName;
    public MonsterName Name => monsterName;

    //[SerializeField] private GameObject prefab;

    //public GameObject Prefab => prefab;

    [SerializeField] private MonsterStatsSO stats;

    public MonsterStatsSO Stats => stats;

    [SerializeField] private List<SuperPower> superPowers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void UseSuperPower(int superPowerNumber)
    {
        if (superPowers == null) return;
        superPowers[superPowerNumber].UseSuperPower();
    }
}
