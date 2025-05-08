using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : StageObject
{
    public Plate objectPrefab;
    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    public Material jokerMaterial;
    public Transform spawnPoint;
    public Transform endPoint;
    public Vector3 move;
    public float spawnDelay;

    private GameManager gameManager;
    private List<Plate> plates = new List<Plate>(10);
    private Block[] blocks;
    private List<Material> materials;
    private IntClusterRow colorNumRow;
    private float elapsedTime;

    protected override int IntroductionStage => 2;

    private int AdvancedStage => 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        blocks = FindObjectsByType<Block>(FindObjectsSortMode.None);

        materials = new List<Material>
        {
            redMaterial,
            greenMaterial,
            blueMaterial,
            jokerMaterial
        };

        colorNumRow = new IntClusterRow(
            (1, 2),
            (2, 2),
            (3, 2),
            (4, 1)
        );
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlateSpawner();
        MoveObjects(move);
    }

    private void UpdatePlateSpawner()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= spawnDelay)
        {
            SpawnPlate();
            elapsedTime = 0;
        }
    }

    private void SpawnPlate()
    {
        int colorNum = colorNumRow.GetRandom().GetValueOrDefault();
        Plate newPlate = Instantiate(objectPrefab);

        if (gameManager.CurrentStage < AdvancedStage || colorNum <= 0)
        {
            newPlate.InitSimple();
        }
        else
        {
            newPlate.Init(materials[colorNum - 1], (Block.Colors)colorNum, blocks);
        }

        newPlate.transform.position = spawnPoint.position;
        plates.Add(newPlate);
    }

    private void MoveObjects(Vector3 move)
    {
        for (int i = plates.Count - 1; i >= 0; i--)
        {
            plates[i].Move(move);
            if (plates[i].IsOverLimit(endPoint.position.x))
            {
                Destroy(plates[i].gameObject);
                plates.RemoveAt(i);
            }
        }
    }

    public override void HandleStageChange(int stageNumber)
    {
        bool show = stageNumber >= IntroductionStage;

        foreach (Plate plate in plates)
        {
            if (show && stageNumber < AdvancedStage)
            {
                plate.MakeSimple();
            }

            plate.gameObject.SetActive(show);
        }

        gameObject.SetActive(show);
    }
}
