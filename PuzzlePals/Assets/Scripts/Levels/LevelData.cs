using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public SceneAsset scene;
    public ushort bestCompletionTime = ushort.MaxValue;
    public byte stars;
    public List<byte> timeForStars;
}
