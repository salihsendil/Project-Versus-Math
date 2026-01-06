public class AnswerEvaluationResultSignal {
    public PlayerRoundData Player;
    public int Answer;
    public bool IsCorrect;

    public AnswerEvaluationResultSignal(PlayerRoundData playerData, int answer, bool isCorrect)
    {
        Player = playerData;
        Answer = answer;
        IsCorrect = isCorrect;
    }
}