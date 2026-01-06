public class PlayerAnswerSubmitted
{
    public PlayerRoundData Player;
    public int Answer;

    public PlayerAnswerSubmitted(PlayerRoundData player, int answer)
    {
        Player = player;
        Answer = answer;
    }
}