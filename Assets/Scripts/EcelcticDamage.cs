using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcelcticDamage : MonoBehaviour
{
    [SerializeField] private Renderer[] _materials;
    [SerializeField] private float _timeChangeColor;
    [SerializeField] private float _timeEnable;

    private Color _defaultColor;

    private Coroutine _activeCoroutineChangeColor;
    private Coroutine _mainActiveCoroutine;

    private void OnEnable()
    {
        foreach (var item in _materials)
        {
            _defaultColor = item.material.color;
        }

        _mainActiveCoroutine = StartCoroutine(TimeToDiable());
    }

    private IEnumerator TimeToDiable()
    {
        _activeCoroutineChangeColor = StartCoroutine(ChangeColor());

        yield return new WaitForSeconds(_timeEnable);

        StopCoroutine(_mainActiveCoroutine);
        StopCoroutine(_activeCoroutineChangeColor);
        
        foreach (var item in _materials)
        {
            item.material.color=_defaultColor;
        }
        
        GetComponent<EcelcticDamage>().enabled = false;
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].material.color = Random.ColorHSV(0, 1);
            }

            yield return new WaitForSeconds(_timeChangeColor);
        }
    }
}
