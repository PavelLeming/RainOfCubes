using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChancheColor()
    {
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
