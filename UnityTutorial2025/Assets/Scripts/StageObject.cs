using UnityEngine;

public abstract class StageObject : MonoBehaviour
{
    protected abstract int IntroductionStage { get; }

    public virtual void HandleStageChange(int stageNumber)
    {
        gameObject.SetActive(stageNumber >= IntroductionStage);
    }
}
