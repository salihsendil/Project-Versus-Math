using System.Collections.Generic;
using UnityEngine;

public class GameplayUIView : MonoBehaviour
{
    [SerializeField] private PlayerRoundUI playerOneUI;
    [SerializeField] private PlayerRoundUI playerTwoUI;

    private Dictionary<PlayerRoundData, PlayerRoundUI> players = new();

    public void SetVisibility(bool isVisible)
    {
        if (gameObject.activeSelf == isVisible) return;
        gameObject.SetActive(isVisible);
    }

    public void SetPlayersData(MatchupData matchup)
    {
        playerOneUI.SetPlayerData(matchup.playerOne);
        playerTwoUI.SetPlayerData(matchup.playerTwo);

        players[matchup.playerOne] = playerOneUI;
        players[matchup.playerTwo] = playerTwoUI;
    }

    public void SetPlayersQuestion(QuestionData question)
    {
        foreach (var player in players)
        {
            player.Key.HasAnswered = false;
            player.Value.SetScreen(question);
        }
    }

    public void DisablePlayerUIButtons(PlayerRoundData playerData)
    {
        players[playerData].DisableButtons();
    }

    public void FinalizePlayersButtons(PlayerRoundData player, int answer, bool isCorrect)
    {
        if (!isCorrect)
        {
            players[player].FinalizeButtonState(answer, isCorrect);
            return;
        }

        players[player].FinalizeButtonState(answer, isCorrect);
        foreach (var p in players)
        {
            p.Value.DisableButtons();
        }
    }
}
