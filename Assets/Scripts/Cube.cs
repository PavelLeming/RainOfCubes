using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;
    private bool _isUncolided = true;
    public Renderer Renderer { get; private set; }
    public Rigidbody Rigidbody {get; private set;}

    public event System.Action<Cube> ReadyForRelease;

    public void Initialize(bool isColided, Renderer renderer, Rigidbody rigidbody)
    {
        _isUncolided = isColided;
        Renderer = renderer;
        Rigidbody = rigidbody;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Platform platform;

        if (collision.gameObject.TryGetComponent<Platform>(out platform) && _isUncolided)
        {
            _colorChanger.ChancheColor();
            _isUncolided = false;
            StartCoroutine(CountdownForRealise());
        }
    }

    private IEnumerator CountdownForRealise()
    {
        var wait = new WaitForSeconds(Random.Range(2, 6));

        yield return wait;

        ReadyForRelease?.Invoke(gameObject.GetComponent<Cube>());
    }
}
