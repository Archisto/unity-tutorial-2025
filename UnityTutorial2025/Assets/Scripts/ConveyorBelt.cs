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

    private List<Plate> plates = new List<Plate>(10);
    private Block[] blocks;
    private List<Material> materials;
    private float elapsedTime;

    protected override int IntroductionStage => 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blocks = FindObjectsByType<Block>(FindObjectsSortMode.None);

        materials = new List<Material>
        {
            redMaterial,
            greenMaterial,
            blueMaterial,
            jokerMaterial
        };
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlateSpawner();
        MoveObjectsAlong();
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
        int randomNum = Random.Range(0, 4);
        Plate newPlate = Instantiate(objectPrefab);
        newPlate.Init(materials[randomNum], (Block.Colors)(randomNum + 1), blocks);
        newPlate.transform.position = spawnPoint.position;
        plates.Add(newPlate);
    }

    private void MoveObjectsAlong()
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

    public override void Show(int stageNumber)
    {
        bool show = stageNumber >= IntroductionStage;

        foreach (Plate plate in plates)
        {
            plate.gameObject.SetActive(show);
        }

        gameObject.SetActive(show);
    }
}
