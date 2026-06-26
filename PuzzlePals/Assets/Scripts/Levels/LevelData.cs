using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public string sceneName;
    [HideInInspector]
    public ushort bestCompletionTime;
    [HideInInspector]
    public byte stars;
    public List<byte> timeForStars;
}
