using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTime : MonoBehaviour
{
    [SerializeField] private float _timeDelay = 0.5f;
    private WaitForSeconds _delay = new WaitForSeconds(0);

    private void OnEnable()
    {
        StartCoroutine(DelayEffect());
    }

    private void Awake()
    {
        _delay = new WaitForSeconds(_timeDelay);
    }

    private IEnumerator DelayEffect()
    {
        yield return _delay;

        gameObject.SetActive(false);
    }
}
