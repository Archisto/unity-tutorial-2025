using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int defaultStage = 4;
    public int defaultLives = 3;

    private UIManager UI;
    private StageObject[] stageObjects;
    private ConveyorBelt conveyorBelt;

    private int lives;
    private int score;

    public int CurrentStage { get; private set; }

    public int LivesIntroductionStage => 4;

    public int Lives
    {
        get => lives;

        set
        {
            lives = value;
            UI.UpdateLivesText(value);
        }
    }

    public int Score
    {
        get => score;

        set
        {
            score = value;
            UI.UpdateScoreText(value);
        }
    }

    public bool IsGameOver { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UI = FindFirstObjectByType<UIManager>();
        UI.Init(LivesIntroductionStage);

        stageObjects = FindObjectsByType<StageObject>(FindObjectsSortMode.None);
        conveyorBelt = FindFirstObjectByType<ConveyorBelt>();

        SetStage(defaultStage);
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdateStageSwitching();
    }

    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void UpdateStageSwitching()
    {
        int stageNumber = 0;

             if (Input.GetKeyDown(KeyCode.Alpha1)) stageNumber = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) stageNumber = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) stageNumber = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) stageNumber = 4;

        if (stageNumber > 0 && stageNumber != CurrentStage)
        {
            SetStage(stageNumber);
        }
    }

    private void SetStage(int stageNumber)
    {
        CurrentStage = stageNumber;

        foreach (StageObject stageObject in stageObjects)
        {
            stageObject.HandleStageChange(stageNumber);
        }
    }

    private void StartGame()
    {
        Lives = defaultLives;
        Score = 0;
        IsGameOver = false;
    }
    
    private void RestartGame()
    {
        conveyorBelt.DestroyPlates();
        StartGame();
    }

    public void LoseGame()
    {
        IsGameOver = true;
        Lives = 0;
        conveyorBelt.DestroyPlates();
    }

    public void LoseLife()
    {
        if (IsGameOver || CurrentStage < LivesIntroductionStage)
        {
            return;
        }

        Lives--;

        if (Lives <= 0)
        {
            LoseGame();
        }
    }

    public void GainScore(int score)
    {
        if (IsGameOver)
        {
            return;
        }

        Score += score;
    }
}
