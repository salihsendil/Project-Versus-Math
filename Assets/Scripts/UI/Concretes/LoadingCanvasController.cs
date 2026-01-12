using TMPro;
using UnityEngine;
using DG.Tweening;

public class LoadingCanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    private Tween _textTween;

    private void OnEnable()
    {
        _textTween?.Kill();
        loadingText.text = "";

        _textTween = DOTween.To(() => loadingText.text, x => loadingText.text = x, "Yükleniyor...", 1f)
            .SetEase(Ease.Linear)
            .SetDelay(0.3f)
            .OnComplete(() =>
            {
                loadingText.DOFade(0f, 0.2f);
            });
    }

    private void OnDisable()
    {
        _textTween?.Kill();
        loadingText.alpha = 1f;
    }
}