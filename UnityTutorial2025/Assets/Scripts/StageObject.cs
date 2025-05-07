using UnityEngine;

public abstract class StageObject : MonoBehaviour
{
    protected abstract int IntroductionStage { get; }

    public virtual void Show(int stageNumber)
    {
        gameObject.SetActive(stageNumber >= IntroductionStage);
    }
}
