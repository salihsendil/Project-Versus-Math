using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RoundActionButton : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [SerializeField] private Button button;
    [SerializeField] private TMP_Text buttonText;

    private PlayerRoundData owner;
    private int buttonValue;

    public void SetOwner(PlayerRoundData player) => owner = player;
    public bool IsOwnedBy(int value) => buttonValue == value;

    public void SetButton(int value)
    {
        button.interactable = true;
        button.image.color = Color.white;

        buttonValue = value;
        buttonText.text = buttonValue.ToString();
    }

    public void DisableButton()
    {
        button.interactable = false;
    }

    public void OnActionButtonPressed()
    {
        signalBus.Fire(new PlayerAnswerSubmitted(owner, buttonValue));
    }

    public void VisualizeButtonResult(bool isCorrect)
    {
        button.image.color = isCorrect ? Color.green : Color.red;
    }
}
