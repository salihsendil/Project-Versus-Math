using UnityEngine;
using Zenject;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class PopupPanelController : MonoBehaviour
{
    [Inject] private SceneService sceneService;

    [SerializeField] private RectTransform rectTransform;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetVisibility(bool isVisible)
    {
        if (gameObject.activeSelf == isVisible) return;
        gameObject.SetActive(isVisible);
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;

        rectTransform.DOScale(Vector3.one, 0.3f)
            .SetEase(Ease.InOutCubic);
    }

    public async void OnYesButtonPressed()
    {
        await sceneService.LoadSceneWithLoading(ScenesEnum.MainMenu);
    }

    public void OnNoButtonPressed()
    {
        rectTransform.DOScale(Vector3.zero, 0.3f)
            .SetEase(Ease.InOutCubic)
            .SetUpdate(true)
            .OnComplete(() => {
                gameObject.SetActive(false);
            });
    }
}
