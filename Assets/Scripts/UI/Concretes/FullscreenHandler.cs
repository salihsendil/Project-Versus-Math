using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FullscreenHandler : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

}
