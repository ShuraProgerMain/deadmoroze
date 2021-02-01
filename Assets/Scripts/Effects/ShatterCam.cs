using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterCam : MonoBehaviour
{

    [SerializeField] private StressReceiver _receiver;

    [SerializeField] private float MaximumStress = 0.6f;
    [SerializeField] private float Range = 45;

    public void OnShake()
    {
        float distance = Vector3.Distance(transform.position, _receiver.gameObject.transform.position);
        float distance01 = Mathf.Clamp01(distance / Range);
        float stress = (1 - Mathf.Pow(distance01, 2)) * MaximumStress;
        _receiver.InduceStress(stress);
    }
}
