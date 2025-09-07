using TMPro;
using UnityEngine;

public class UI_Maneger : MonoBehaviour
{
    public static UI_Maneger instance;
    public PlayerController playerController;
    [SerializeField] TextMeshProUGUI scoreText, levelPanelText;
    [SerializeField] GameObject levelPanel;
    [SerializeField] int coinScore;
    [SerializeField] float playerHealth;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        Event_Maneger.Subscribe("ScoreUpdate", Score);
        Event_Maneger.Subscribe("PlayerHealth", PlayerHealthUpdate);
        Event_Maneger.Subscribe("LevelFailCall", LevelFail);
        Event_Maneger.Subscribe("LevelCompleteCall", LevelComplete);
    }
    private void PlayerHealthUpdate(object scoreCoin)
    {
        playerHealth = (float)scoreCoin;
    }

    private void Score(object obj)
    {
        coinScore++;
        scoreText.text = coinScore.ToString("0");
        if (coinScore >= 4)
        {
            Event_Maneger.Trigger("LevelCompleteCall");
        }

    }
    private void OnDisable()
    {
        Event_Maneger.Unsubscribe("ScoreUpdate", Score);
        Event_Maneger.Unsubscribe("PlayerHealth", PlayerHealthUpdate);
        Event_Maneger.Unsubscribe("LevelFailCall", LevelFail);
        Event_Maneger.Unsubscribe("LevelCompleteCall", LevelComplete);
    }

    public void LevelComplete(object obj)
    {
        levelPanel.SetActive(true);
        levelPanelText.text = "Level Complete";
        //Event_Maneger.Trigger("LevelCompleteCall");
    }

    public void LevelFail(object obj)
    {
        levelPanel.SetActive(true);
        levelPanelText.text = "Level Fail";
        //Event_Maneger.Trigger("LevelFailCall");
        //Event_Maneger.LevelFailCall();
    }

}
