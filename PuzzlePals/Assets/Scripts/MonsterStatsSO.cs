using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStats", menuName = "ScriptableObjects/MonsterStats")]
public class MonsterStatsSO : ScriptableObject
{
    public float movementSpeed;
    public float sprintMultiplier;

    [Tooltip("The amount of units the jump is going to be.")]
    public float jumpHeight;

    [Tooltip("Increases the falling speed.")]
    public float fallMultiplier;
}
