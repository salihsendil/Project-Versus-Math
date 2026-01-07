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

    [Header("Size")]
    [SerializeField] private TMP_Text sizeText;
    [SerializeField] private Button sizeRemoveButton;
    [SerializeField] private Button sizeAddButton;

    [Header("Question Count")]
    [SerializeField] private TMP_Text questionCountText;
    [SerializeField] private Button questionCountRemoveButton;
    [SerializeField] private Button questionCountAddButton;

    [Header("MinLimit")]
    [SerializeField] private TMP_Text minLimitText;
    [SerializeField] private Button minLimitRemoveButton;
    [SerializeField] private Button minLimitAddButton;

    [Header("MaxLimit")]
    [SerializeField] private TMP_Text maxLimitText;
    [SerializeField] private Button maxLimitRemoveButton;
    [SerializeField] private Button maxLimitAddButton;

    [Header("Operations")]
    [SerializeField] private OperationSelectButton additionToogle;
    [SerializeField] private OperationSelectButton subtractionToggle;
    [SerializeField] private OperationSelectButton multiplicationToggle;
    [SerializeField] private OperationSelectButton divisionToggle;

    public void OnEnable()
    {
        signalBus.Subscribe<OperationsSelectionChangedSignal>(SetOperationSelection);
    }

    public void OnDisable()
    {
        signalBus.Unsubscribe<OperationsSelectionChangedSignal>(SetOperationSelection);
    }

    private void Awake()
    {
        sizeText.text = gameConfig.TournamentSize.ToString();
        questionCountText.text = gameConfig.QuestionCountPerRound.ToString();
        minLimitText.text = gameConfig.MinNumberLimit.ToString();
        maxLimitText.text = gameConfig.MaxNumberLimit.ToString();

        additionToogle.SetButtonImage(gameConfig.HasAllowedAddition);
        subtractionToggle.SetButtonImage(gameConfig.HasAllowedSubtraction);
        multiplicationToggle.SetButtonImage(gameConfig.HasAllowedMultiplication);
        divisionToggle.SetButtonImage(gameConfig.HasAllowedDivision);
    }


    public void ChangeSetTournamentSize(int value)
    {
        gameConfig.TournamentSize += value;
        gameConfig.TournamentSize = Mathf.Clamp(gameConfig.TournamentSize, 2, 50);
        sizeText.text = gameConfig.TournamentSize.ToString();
    }

    public void ChangeSetQuestionCount(int value)
    {
        gameConfig.QuestionCountPerRound += value;
        gameConfig.QuestionCountPerRound = Mathf.Clamp(gameConfig.QuestionCountPerRound, 1, 20);
        questionCountText.text = gameConfig.QuestionCountPerRound.ToString();
    }

    public void SetMinLimit(int value) => ChangeSetLimit(0, value);
    public void SetMaxLimit(int value) => ChangeSetLimit(1, value);
    private void ChangeSetLimit(int id, int value)
    {
        if (id == 0)
        {
            gameConfig.MinNumberLimit += value;
            gameConfig.MinNumberLimit = Mathf.Clamp(gameConfig.MinNumberLimit, -100, gameConfig.MaxNumberLimit - 1);
            minLimitText.text = gameConfig.MinNumberLimit.ToString();
        }

        else if (id == 1)
        {
            gameConfig.MaxNumberLimit += value;

            if (gameConfig.MaxNumberLimit <= gameConfig.MinNumberLimit)
            {
                gameConfig.MinNumberLimit--;
                minLimitText.text = gameConfig.MinNumberLimit.ToString();
            }

            maxLimitText.text = gameConfig.MaxNumberLimit.ToString();
        }
    }


    private int GetAllowedOperationCount()
    {
        int count = 0;
        if (gameConfig.HasAllowedAddition) count++;
        if (gameConfig.HasAllowedSubtraction) count++;
        if (gameConfig.HasAllowedMultiplication) count++;
        if (gameConfig.HasAllowedDivision) count++;
        return count;
    }
    private OperationSelectButton GetToggleByOperation(OperationTypes operation)
    {
        return operation switch
        {
            OperationTypes.Addition => additionToogle,
            OperationTypes.Subtraction => subtractionToggle,
            OperationTypes.Multiplication => multiplicationToggle,
            OperationTypes.Division => divisionToggle,
            _ => null
        };
    }
    public void SetOperationSelection(OperationsSelectionChangedSignal signal)
    {
        if (!signal.isAllowed && GetAllowedOperationCount() <= 1)
        {
            GetToggleByOperation(signal.operation).SetButtonImage(true);
            return;
        }

        switch (signal.operation)
        {
            case OperationTypes.Addition:
                gameConfig.HasAllowedAddition = signal.isAllowed;
                break;
            case OperationTypes.Subtraction:
                gameConfig.HasAllowedSubtraction = signal.isAllowed;
                break;
            case OperationTypes.Multiplication:
                gameConfig.HasAllowedMultiplication = signal.isAllowed;
                break;
            case OperationTypes.Division:
                gameConfig.HasAllowedDivision = signal.isAllowed;
                break;
            default:
                break;
        }
    }

    public void OnStartButtonClicked()
    {
        signalBus.Fire(new LobbySetupRequestedSignal());
        sceneService.LoadSceneWithLoading(ScenesEnum.Lobby);
    }

    public void OnQuitCuttonClicked()
    {
        sceneService.QuitGame();
    }
}
