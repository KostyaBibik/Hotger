using UnityEngine;

[CreateAssetMenu(fileName = "New DifficultLevel", menuName = "DifficultLevel", order = 20)]
public class DifficultLevel : ScriptableObject
{
    public Сomplexity difficulty;
    public float intervalSpawnObstacles;
    public float intervalSpeedRaise;
    public float speedIncreaseValue;
}

public enum Сomplexity
{
    Easy,
    Normal,
    Hard
}
