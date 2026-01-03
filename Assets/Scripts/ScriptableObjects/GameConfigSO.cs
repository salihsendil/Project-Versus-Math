using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameConfigSO", menuName = "Scriptable Objects/New GameConfigSO")]
public class GameConfigSO : ScriptableObject
{
    public int TournamentSize = 32;
    public int MinNumberLimit = 0;
    public int MaxNumberLimit = 20;
    public int QuestionCountPerRound = 10;
    public bool HasAllowedAddition = true;
    public bool HasAllowedSubtraction = true;
    public bool HasAllowedMultiplication = true;
    public bool HasAllowedDivision = true;
}
