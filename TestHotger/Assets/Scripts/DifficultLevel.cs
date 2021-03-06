using UnityEngine;

[CreateAssetMenu(fileName = "New DifficultLevel", menuName = "DifficultLevel", order = 20)]
public class DifficultLevel : ScriptableObject
{
    public –°omplexity difficulty;
    public float intervalSpawnObstacles;
    public float intervalSpeedRaise;
    public float speedIncreaseValue;
}

public enum –°omplexity
{
    Easy,
    Normal,
    Hard
}
