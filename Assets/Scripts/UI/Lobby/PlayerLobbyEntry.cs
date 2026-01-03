using TMPro;
using UnityEngine;

public class PlayerLobbyEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        SetEntryActive(false);
    }

    public void SetEntryActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetText(string value)
    {
        text.text = value;
    }
}
