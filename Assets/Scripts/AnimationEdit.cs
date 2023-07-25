using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEdit : MonoBehaviour
{
    [SerializeField] private VacuumCleaner vacuumCleaner;
    [SerializeField] private float _timeChangeScale;

    private Coroutine _activeCoroutine;
    private Vector3 _numberNewLocalScaleAdded = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 _newCountScale;

    public VacuumCleaner PlayerCleaner => vacuumCleaner;

    void Start()
    {
        _newCountScale = transform.localScale;
    }

    public void TakeLooseObjectSize()
    {
        _newCountScale -= _numberNewLocalScaleAdded;
        Restart(_newCountScale);
    }

    public void TakeNewObjectSize()
    {
        _newCountScale += _numberNewLocalScaleAdded;
        Restart(_newCountScale);
    }

    private void Restart(Vector3 target)
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }

        _activeCoroutine = StartCoroutine(ChangeScale(target));
    }

    private IEnumerator ChangeScale(Vector3 target)
    {
        while (transform.localScale != target)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime);
            yield return null;
        }

        transform.localScale = _newCountScale;
    }
}
