using UnityEngine;

public class Spinner : StageObject
{
    public Transform redBlock;
    public Transform greenBlock;
    public Transform blueBlock;

    public float rotationSpeed = 100;
    public float blockPlacementRadius = 1;
    public float blockPlacementY = 1;

    protected override int IntroductionStage => 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float thirdRotation = 360 / 3;

        PlaceBlock(redBlock,   0 * thirdRotation * Mathf.Deg2Rad);
        PlaceBlock(greenBlock, 1 * thirdRotation * Mathf.Deg2Rad);
        PlaceBlock(blueBlock,  2 * thirdRotation * Mathf.Deg2Rad);
    }

    private Vector3 GetPositionOnCircle(float radius, float angleRad)
    {
        float x = radius * Mathf.Cos(angleRad);
        float z = radius * Mathf.Sin(angleRad);
        return new Vector3(x, 0, z);
    }

    private void PlaceBlock(Transform block, float angle)
    {
        Vector3 blockPosition = GetPositionOnCircle(blockPlacementRadius, angle);
        blockPosition.y = blockPlacementY;
        block.position = blockPosition;

        Vector3 blockLookVector = blockPosition - transform.position;
        blockLookVector.y = 0;
        block.rotation = Quaternion.LookRotation(blockLookVector);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 newRotation = transform.rotation.eulerAngles;
        float input = Input.GetAxisRaw("Horizontal");

        if (input < 0)
        {
            newRotation.y -= rotationSpeed * Time.deltaTime;
        }
        else if (input > 0)
        {
            newRotation.y += rotationSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(newRotation);
    }
}
