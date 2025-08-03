using System.Collections;
using UnityEngine;

public class Cube : PoolableObject
{
    [SerializeField] private ColorChanger _colorChanger;

    private bool _isUncolided = true;
    public event System.Action<Cube> ReadyForRelease;

    public void Initialize(bool isColided)
    {
        _isUncolided = isColided;
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

        ReadyForRelease?.Invoke(this);
    }
}
