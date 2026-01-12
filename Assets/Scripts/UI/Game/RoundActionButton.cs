using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(RectTransform))]
public class RoundActionButton : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [SerializeField] private Button button;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private RectTransform rectTransform;

    private PlayerRoundData owner;
    private int buttonValue;

    private void Awake()
    {
        TryGetComponent(out rectTransform);
    }

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
        if (isCorrect)
        {
            button.image.color = Color.green;
            rectTransform.DOShakeScale(0.5f, 0.2f);
            return;
        }

        button.image.color = Color.red;
        rectTransform.DOShakeAnchorPos(0.5f, 30f, 10, 90);
    }
}
