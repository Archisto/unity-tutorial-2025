using TMPro;
using UnityEngine;

public class UIManager : StageObject
{
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    private int livesIntroductionStage;

    protected override int IntroductionStage => 3;

    public void Init(int livesIntroductionStage)
    {
        this.livesIntroductionStage = livesIntroductionStage;
    }

    public void UpdateLivesText(int lives)
    {
        livesText.text = $"[{lives}]";
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public override void HandleStageChange(int stageNumber)
    {
        livesText.gameObject.SetActive(stageNumber >= livesIntroductionStage);
        scoreText.gameObject.SetActive(stageNumber >= IntroductionStage);
    }
}
