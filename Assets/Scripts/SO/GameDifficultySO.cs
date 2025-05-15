using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty_", menuName = "Scriptable Objects/DifficultyData")]
public class GameDifficultySO : ScriptableObject
{
    public int boardWidth;
    public int boardHeight;
    public int minesAmount;
}
