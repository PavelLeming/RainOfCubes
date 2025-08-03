using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : PoolableObject
{
    [SerializeField] private AlphaChaenger _alphaChaenger;

    private float _minTime = 2f;
    private float _maxTimer = 5f;
    private float _explosionRadius = 10f;
    private float _explosionPower = 500f;
    public event System.Action<Bomb> ReadyForRelease;

    public void StartTimer()
    {
        StartCoroutine(CountTime());
    }

    private IEnumerator CountTime()
    {
        float time = Random.Range(_minTime, _maxTimer);
        var wait = new WaitForSeconds(time);

        _alphaChaenger.StartChangeAlpha(time);

        yield return wait;

        Exploade();
        ReadyForRelease?.Invoke(this);
    }

    private void Exploade()
    {
        foreach (Rigidbody rigitbody in GetExplodableObjects())
        {
            rigitbody.AddExplosionForce(_explosionPower, transform.position, _explosionRadius);
        }
    }

    public List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> objects = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                objects.Add(hit.attachedRigidbody);
            }
        }

        return objects;
    }
}
