using System.Linq;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public Block.Colors color;
    public float triggerProximity = 3;

    private GameManager gameManager;
    private MeshRenderer meshRenderer;
    private Material defaultMaterial;
    private Block[] blocks;
    private Block blockOnPlate;
    private bool isSimple;
    private bool isBlockHandled;

    public void InitSimple()
    {
        isSimple = true;
    }

    public void Init(Material material, Block.Colors color, Block[] blocks)
    {
        gameManager = FindAnyObjectByType<GameManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;
        SetMaterial(material);
        this.color = color;
        this.blocks = blocks;
        blockOnPlate = GetComponentInChildren<Block>(true);
    }

    public void MakeSimple()
    {
        if (isSimple)
        {
            return;
        }

        isSimple = true;
        SetMaterial(defaultMaterial);
        color = Block.Colors.None;
        blockOnPlate.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSimple && !isBlockHandled)
        {
            CheckBlockProximity();
        }
    }

    public void Move(Vector3 move)
    {
        transform.position += move * Time.deltaTime;
    }

    private void CheckBlockProximity()
    {
        Block nearBlock = blocks.
            FirstOrDefault(b => Vector3.Distance(transform.position, b.transform.position) <= triggerProximity);

        if (nearBlock != null)
        {
            isBlockHandled = true;
            blockOnPlate.Init(nearBlock.color, nearBlock.Material);

            if (color == Block.Colors.Any || nearBlock.color == color)
            {
                gameManager.GainScore(1);
            }
        }
    }

    public bool IsOverLimit(float maxX)
    {
        return transform.position.x > maxX;
    }

    public void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }

    private void OnDrawGizmos()
    {
        if (isSimple)
        {
            return;
        }

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, triggerProximity);
    }
}
