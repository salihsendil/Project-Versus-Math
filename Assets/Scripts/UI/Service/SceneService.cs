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

    public async void LoadSceneWithLoading(ScenesEnum sceneName, float duration = 1.5f)
    {
        loadingCanvasGroup.gameObject.SetActive(true);
        await loadingCanvasGroup.DOFade(1, 0.5f).AsyncWaitForCompletion();

        var op = SceneManager.LoadSceneAsync(sceneName.ToString());
        op.allowSceneActivation = false;

        await Task.Delay((int)(duration * 1000));

        while (op.progress < 0.9f) await Task.Yield();

        op.allowSceneActivation = true;
        while (!op.isDone) await Task.Yield();

        await loadingCanvasGroup.DOFade(0, 0.5f).AsyncWaitForCompletion();
        loadingCanvasGroup.gameObject.SetActive(false);
    }

    public async void FadeToTournamentEnd(System.Action onMidFade, float fadeDuration = 0.5f)
    {
        loadingCanvasGroup.gameObject.SetActive(true);
        loadingCanvasGroup.blocksRaycasts = true;

        await loadingCanvasGroup
            .DOFade(1f, fadeDuration)
            .AsyncWaitForCompletion();

        onMidFade?.Invoke();

        await loadingCanvasGroup
            .DOFade(0f, fadeDuration)
            .AsyncWaitForCompletion();

        loadingCanvasGroup.blocksRaycasts = false;
        loadingCanvasGroup.gameObject.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
