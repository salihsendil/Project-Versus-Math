using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;
using Zenject;


public class SceneService : MonoBehaviour, IInitializable
{
    [Inject] private CanvasGroup loadingCanvasGroup;

    public void Initialize()
    {
        loadingCanvasGroup.alpha = 0;
        loadingCanvasGroup.blocksRaycasts = false;
        loadingCanvasGroup.gameObject.SetActive(false);
    }
    public void LoadScene(ScenesEnum sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }

    public async void LoadSceneWithLoading(ScenesEnum sceneName, float duration = 1.5f)
    {
        loadingCanvasGroup.gameObject.SetActive(true);
        await loadingCanvasGroup.DOFade(1, 0.5f).AsyncWaitForCompletion();

        await Task.Delay((int)(duration * 1000));

        var op = SceneManager.LoadSceneAsync(sceneName.ToString());
        while (!op.isDone) await Task.Yield();

        await loadingCanvasGroup.DOFade(0, 0.5f).AsyncWaitForCompletion();
        loadingCanvasGroup.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
