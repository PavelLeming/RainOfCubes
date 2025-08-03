using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChaenger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void StartChangeAlpha(float time)
    {
        StartCoroutine(ChangeAlpha(time));
    }

    private IEnumerator ChangeAlpha(float time)
    {
        float currentTime = time;
        Color color = _renderer.material.color;

        while (color.a > 0f)
        {
            currentTime -= Time.deltaTime;
            float alpha = Mathf.Clamp01(currentTime / time);

            color.a = alpha;
            _renderer.material.color = color;

            yield return null;
        }
    }
}
