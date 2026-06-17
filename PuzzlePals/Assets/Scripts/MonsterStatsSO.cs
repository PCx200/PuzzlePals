using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStats", menuName = "ScriptableObjects/MonsterStats")]
public class MonsterStatsSO : ScriptableObject
{
    public float acceleration;
    public float sprintMultiplier;
    public float airMultiplier;
    public float friction;

    [Tooltip("The amount of units the jump is going to be.")]
    public float jumpHeight;
}
