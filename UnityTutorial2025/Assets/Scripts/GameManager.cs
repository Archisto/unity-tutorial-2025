using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<StageObject> stageObjects = new List<StageObject>(7);
    public int defaultStage = 7;
    public int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetStage(defaultStage);
    }

    // Update is called once per frame
    void Update()
    {
        int stageNumber = 0;

             if (Input.GetKeyDown(KeyCode.Alpha1)) stageNumber = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) stageNumber = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) stageNumber = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) stageNumber = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha5)) stageNumber = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha6)) stageNumber = 6;
        else if (Input.GetKeyDown(KeyCode.Alpha7)) stageNumber = 7;

        if (stageNumber > 0)
        {
            SetStage(stageNumber);
        }
    }

    private void SetStage(int stageNumber)
    {
        foreach (StageObject stageObject in stageObjects)
        {
            stageObject.Show(stageNumber);
        }
    }

    public void GainScore(int score)
    {
        this.score += score;
    }
}
