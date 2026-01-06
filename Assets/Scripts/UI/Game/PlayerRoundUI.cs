using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRoundUI : MonoBehaviour
{
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private List<RoundActionButton> buttons = new();

    public void SetPlayerData(PlayerRoundData player)
    {
        foreach (var button in buttons)
        {
            button.SetOwner(player);
        }
    }

    public void SetScreen(QuestionData question)
    {
        questionText.text = question.QuestionText;

        for (int i = 0; i < question.Answers.Count; i++)
        {
            buttons[i].SetButton(question.Answers[i]);
        }
    }

    public void DisableButtons()
    {
        foreach (var button in buttons)
        {
            button.DisableButton();
        }
    }

    public void FinalizeButtonState(int value, bool isCorrect)
    {
        foreach (var button in buttons)
        {
            if (button.IsOwnedBy(value))
            {
                button.VisualizeButtonResult(isCorrect);
                return;
            }
        }
    }
}
