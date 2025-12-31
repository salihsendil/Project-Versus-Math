using TMPro;
using UnityEngine;

public class LobbyPlayerItem : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName;

    public void SetPlayersName(string name)
    {
        playerName.text = name;
    }
}
