using UnityEngine;

public class Block : MonoBehaviour
{
    public enum Colors
    {
        None,
        Red,
        Green,
        Blue,
        Any
    }

    public Colors color;

    private MeshRenderer meshRenderer;

    public Material Material
    {
        get
        {
            return meshRenderer.material;
        }

        set
        {
            meshRenderer.material = value;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    public void Init()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Init(Colors color, Material material)
    {
        Init();
        this.color = color;
        Material = material;
        gameObject.SetActive(true);
    }
}
