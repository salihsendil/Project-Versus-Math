using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;
using Zenject;

public class SceneService : MonoBehaviour, IInitializable
{
    [Inject] private CanvasGroup loadingCanvasGroup;

    private void Awake()
    {
        loadingCanvasGroup.alpha = 0;
        loadingCanvasGroup.gameObject.SetActive(false);
    }

    public void Initialize()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(loadingCanvasGroup.gameObject);

        loadingCanvasGroup.blocksRaycasts = false;
    }

    public async Task LoadSceneWithLoading(ScenesEnum sceneName, float duration = 2f)
    {
        loadingCanvasGroup.gameObject.SetActive(true);
        loadingCanvasGroup.blocksRaycasts = true;

        await loadingCanvasGroup.DOFade(1f, 0.5f).AsyncWaitForCompletion();

        var op = SceneManager.LoadSceneAsync(sceneName.ToString());
        op.allowSceneActivation = false;

        float startTime = Time.time;

#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL: Ana thread'i canlý tutmak için frame bazlý bekleme
        while (op.progress < 0.9f)
        {
            await Task.Yield();
        }
#else
        while (op.progress < 0.9f)
        {
            await Task.Delay(100);
        }
#endif

        float remainingTime = (startTime + duration) - Time.time;
        if (remainingTime > 0)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            float timer = 0;
            while (timer < remainingTime)
            {
                timer += Time.deltaTime;
                await Task.Yield();
            }
#else
            await Task.Delay((int)(remainingTime * 1000));
#endif
        }

        op.allowSceneActivation = true;

        while (!op.isDone)
        {
            await Task.Yield();
        }

        await Task.Yield();

        await loadingCanvasGroup.DOFade(0f, 0.5f).AsyncWaitForCompletion();

        loadingCanvasGroup.blocksRaycasts = false;
        loadingCanvasGroup.gameObject.SetActive(false);
    }

    public async Task FadeToPanelTransition(System.Action onMidFade, float fadeDuration = 1f)
    {
        loadingCanvasGroup.gameObject.SetActive(true);
        loadingCanvasGroup.blocksRaycasts = true;

        await loadingCanvasGroup.DOFade(1f, fadeDuration).AsyncWaitForCompletion();

        onMidFade?.Invoke();

        await loadingCanvasGroup.DOFade(0f, fadeDuration).AsyncWaitForCompletion();

        loadingCanvasGroup.blocksRaycasts = false;
        loadingCanvasGroup.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        if (Screen.fullScreen) { Screen.fullScreen = false; }
#else
        Application.Quit();
#endif
    }
}
