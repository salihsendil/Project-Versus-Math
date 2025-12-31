using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuUIController : MonoBehaviour
{
    //References
    [Inject] private SignalBus signalBus;
    [Inject] private GameConfigSO gameConfig;
    [Inject] private SceneService sceneService;

    //Size Elements
    [SerializeField] private Button removeButton;
    [SerializeField] private Button addButton;
    [SerializeField] private TMP_Text sizeText;

    //MinMax Value Elements
    [SerializeField] private TMP_InputField minValueInputField;
    [SerializeField] private TMP_InputField maxValueInputField;

    //Allow Operations
    [SerializeField] private Toggle additionToogle;
    [SerializeField] private Toggle subtractionToogle;
    [SerializeField] private Toggle multiplicationToogle;
    [SerializeField] private Toggle divisionToogle;

    //Buttons
    [SerializeField] private Button startGameButton;

    private void Start()
    {
        SetUIElements();
    }

    private void SetUIElements()
    {
        sizeText.text = gameConfig.TournamentSize.ToString();
        minValueInputField.text = gameConfig.MinNumberLimit.ToString();
        maxValueInputField.text = gameConfig.MaxNumberLimit.ToString();
        additionToogle.isOn = gameConfig.HasAllowedAddition;
        subtractionToogle.isOn = gameConfig.HasAllowedSubtraction;
        multiplicationToogle.isOn = gameConfig.HasAllowedMultiplication;
        divisionToogle.isOn = gameConfig.HasAllowedDivision;
    }

    #region AllowedOperations
    public void SetAdditionOperation(bool isToggle) => SetOperations(1, isToggle);
    public void SetSubtractionOperation(bool isToggle) => SetOperations(2, isToggle);
    public void SetMultiplicationOperation(bool isToggle) => SetOperations(3, isToggle);
    public void SetDivisionOperation(bool isToggle) => SetOperations(4, isToggle);
    private void SetOperations(int id, bool isToggle)
    {
        switch (id)
        {
            case 1:
                gameConfig.HasAllowedAddition = isToggle;
                break;
            case 2:
                gameConfig.HasAllowedSubtraction = isToggle;
                break;
            case 3:
                gameConfig.HasAllowedMultiplication = isToggle;
                break;
            case 4:
                gameConfig.HasAllowedDivision = isToggle;
                break;
            default:
                break;
        }
    }
    #endregion

    #region TournamentSize
    public void SetPlayerSize(int value)
    {
        gameConfig.TournamentSize += value;
        sizeText.text = gameConfig.TournamentSize.ToString();
    }
    #endregion

    #region MinMaxLimit
    public void SetMinLimit(string val) => SetLimitValues(0, int.Parse(val));
    public void SetMaxLimit(string val) => SetLimitValues(1, int.Parse(val));
    private void SetLimitValues(int id, int value)
    {
        if (id == 0)
        {
            gameConfig.MinNumberLimit = value;
        }
        else if (id == 1)
        {
            gameConfig.MaxNumberLimit = value;
        }
    }
    #endregion

    #region StartButton
    public void StartButton()
    {
        signalBus.Fire<LoadLobbyRequest>();
        sceneService.LoadSceneWithLoading(ScenesEnum.LobbyScene);
    }
    #endregion
}
