using TMPro;
using UnityEngine;

public class UI_Maneger : MonoBehaviour
{
    public static UI_Maneger instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score, scoreAdd;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        Coin_Trigger_Event.coinTrigger += Score;

    }
    private void Score()
    {
        score++;
        scoreText.text = score.ToString("0.0");

    }
    private void OnDisable()
    {
        Coin_Trigger_Event.coinTrigger -= Score;
    }

}
