using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    private int _liveTime = 4;
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
        if (collision.gameObject.GetComponent<Platform>() != null && _isUncolided)
        {
            Renderer.material.color = new Color(Random.value, Random.value, Random.value);
            _isUncolided = false;
            StartCoroutine(CountdownForRealise());
        }
    }

    private IEnumerator CountdownForRealise()
    {
        var wait = new WaitForSeconds(1);

        for (int i = _liveTime; i > 0 ; i--)
        {
            yield return wait;
        }

        ReadyForRelease?.Invoke(gameObject.GetComponent<Cube>());
    }
}
