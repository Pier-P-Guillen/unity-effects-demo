using System.Collections;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] GameObject[] _particles = null;

    private bool _effectsOn = false;

    private void OnMouseDown()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        if (_effectsOn)
        {
            yield break;
        }

        _effectsOn = true;

        var renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_IsShaking", 1);

        foreach (var particle in _particles)
        {
            particle.SetActive(false);
        }

        CameraController.Instance.ShakeCamera(5, 2);
        yield return new WaitForSeconds(2);

        renderer.material.SetFloat("_IsShaking", 0);
        foreach (var particle in _particles)
        {
            particle.SetActive(true);
        }

        _effectsOn = false;
    }
}