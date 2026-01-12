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
    [Inject] private SoundDataSO soundData;
    [Inject] private AudioService audioService;

    [Header("Size")]
    [SerializeField] private TMP_Text tournamentSizeText;

    [Header("Question Count")]
    [SerializeField] private TMP_Text questionCountText;

    [Header("MinLimit")]
    [SerializeField] private TMP_Text minLimitText;

    [Header("MaxLimit")]
    [SerializeField] private TMP_Text maxLimitText;

    [Header("Operations")]
    [SerializeField] private OperationSelectButton additionToogle;
    [SerializeField] private OperationSelectButton subtractionToggle;
    [SerializeField] private OperationSelectButton multiplicationToggle;
    [SerializeField] private OperationSelectButton divisionToggle;

    public void OnEnable()
    {
        signalBus.Subscribe<OperationsSelectionChangedSignal>(SetOperationSelection);
        signalBus.Subscribe<OnUIValueChangesButtonSignal>(HandleDataAdjustment);
    }

    public void OnDisable()
    {
        signalBus.Unsubscribe<OperationsSelectionChangedSignal>(SetOperationSelection);
        signalBus.Unsubscribe<OnUIValueChangesButtonSignal>(HandleDataAdjustment);
    }

    private void Awake()
    {
        tournamentSizeText.text = gameConfig.TournamentSize.ToString();
        questionCountText.text = gameConfig.QuestionCountPerRound.ToString();
        minLimitText.text = gameConfig.MinNumberLimit.ToString();
        maxLimitText.text = gameConfig.MaxNumberLimit.ToString();

        additionToogle.SetButtonVisual(gameConfig.HasAllowedAddition);
        subtractionToggle.SetButtonVisual(gameConfig.HasAllowedSubtraction);
        multiplicationToggle.SetButtonVisual(gameConfig.HasAllowedMultiplication);
        divisionToggle.SetButtonVisual(gameConfig.HasAllowedDivision);
    }

    private void HandleDataAdjustment(OnUIValueChangesButtonSignal signal)
    {
        switch (signal.ButtonType)
        {
            case UIButtonDataType.QuestionCountSetter:
                SetQuestionCount(signal.Value);
                break;
            case UIButtonDataType.TournamentSizeSetter:
                SetTournamentSize(signal.Value);
                break;
            case UIButtonDataType.MinLimitSetter:
                SetLimits(0, signal.Value);
                break;
            case UIButtonDataType.MaxLimitSetter:
                SetLimits(1, signal.Value);
                break;
            default:
                break;
        }
        audioService.PlaySfx(soundData.buttonClick, 1f);
    }

    private void SetQuestionCount(int value)
    {
        gameConfig.QuestionCountPerRound += value;
        gameConfig.QuestionCountPerRound = Mathf.Clamp(gameConfig.QuestionCountPerRound, 1, 20);
        questionCountText.text = gameConfig.QuestionCountPerRound.ToString();
    }

    private void SetTournamentSize(int value)
    {
        gameConfig.TournamentSize += value;
        gameConfig.TournamentSize = Mathf.Clamp(gameConfig.TournamentSize, 2, 64);
        tournamentSizeText.text = gameConfig.TournamentSize.ToString();
    }

    private void SetLimits(int id, int value)
    {
        if (id == 0)
        {
            gameConfig.MinNumberLimit += value;
            gameConfig.MinNumberLimit = Mathf.Clamp(gameConfig.MinNumberLimit, -100, gameConfig.MaxNumberLimit - 3);
            minLimitText.text = gameConfig.MinNumberLimit.ToString();
        }

        else if (id == 1)
        {
            gameConfig.MaxNumberLimit += value;
            gameConfig.MaxNumberLimit = Mathf.Clamp(gameConfig.MaxNumberLimit, gameConfig.MinNumberLimit + 3, 100);

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
        if (!signal.IsAllowed && GetAllowedOperationCount() <= 1)
        {
            GetToggleByOperation(signal.Operation).SetButtonVisual(true);
            return;
        }

        switch (signal.Operation)
        {
            case OperationTypes.Addition:
                gameConfig.HasAllowedAddition = signal.IsAllowed;
                break;
            case OperationTypes.Subtraction:
                gameConfig.HasAllowedSubtraction = signal.IsAllowed;
                break;
            case OperationTypes.Multiplication:
                gameConfig.HasAllowedMultiplication = signal.IsAllowed;
                break;
            case OperationTypes.Division:
                gameConfig.HasAllowedDivision = signal.IsAllowed;
                break;
            default:
                break;
        }
    }

    public async void OnStartButtonClicked()
    {
        audioService.PlaySfx(soundData.buttonClick);
        signalBus.Fire(new LobbySetupRequestedSignal());
        await sceneService.LoadSceneWithLoading(ScenesEnum.Lobby);
    }

    public void OnQuitCuttonClicked()
    {
        audioService.PlaySfx(soundData.buttonClick);
        sceneService.QuitGame();
    }
}
