using TMPro;
using UnityEngine;

public class UIManager : StageObject
{
    public TextMeshProUGUI scoreText;

    protected override int IntroductionStage => 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText(0);
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public override void HandleStageChange(int stageNumber)
    {
        scoreText.gameObject.SetActive(stageNumber >= IntroductionStage);
    }
}
