using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TournamentEndUIView : MonoBehaviour
{
    [Inject] private SceneService sceneService;

    [SerializeField] private TMP_Text playerName;
    [SerializeField] private Button mainMenuButton;
    public void SetVisibility(bool isVisible)
    {
        if (gameObject.activeSelf == isVisible) return;
        gameObject.SetActive(isVisible);
    }

    public void SetScreen(string winner)
    {
        playerName.text = winner;
    }

    public void OnMainMenuButtonPressed()
    {
        sceneService.LoadSceneWithLoading(ScenesEnum.MainMenu);
    }

}
