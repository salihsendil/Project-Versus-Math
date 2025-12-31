using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    //Text
    [SerializeField] private TMP_Text playerName;

    //Button
    [SerializeField] private Button nextRoundButton;

    private void SetWinnerPlayerName(string name)
    {
        playerName.text = name;
    }

    public void GetNextRoundButton()
    {

    }

}
