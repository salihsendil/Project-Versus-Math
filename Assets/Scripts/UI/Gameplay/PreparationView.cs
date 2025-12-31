using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreparationView : MonoBehaviour
{
    //Text
    [SerializeField] private TMP_Text playerOneName;
    [SerializeField] private TMP_Text playerTwoName;

    //Button
    [SerializeField] private Button startRoundButton;

    private void SetPlayersName(string p1, string p2)
    {
        playerOneName.text = p1;
        playerTwoName.text = p2;
    }

    public void StartRoundButtonClicked()
    {

    }
}
