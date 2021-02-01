using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.Universal;
using UnityEngine.Timeline;

[RequireComponent(typeof(Rigidbody))]
public abstract class AIPathfinder : MonoBehaviour
{

    [HideInInspector] public float _walkSpeed;
    [HideInInspector] public float _rotationSpeed;
    [HideInInspector] public float _angleFOV;
    [HideInInspector] public float _angleRotation;
    [HideInInspector] public Transform _targetPosition;
    
    [SerializeField] private LayerMask _detectLayer;
    [SerializeField] private PathfindData _pathfindData;
    private AIAttack _aiAttack;

    private bool _isMove = true;

    private Rigidbody _rigidbody;
    
    private void Awake() 
    {
        _aiAttack = GetComponent<AIAttack>();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _pathfindData.Initialize(this);
    }

    private void Update()
    {

        var rotation = this.transform.rotation;
        
        
        //То, что ниже. переписать
        var rotationMod = Quaternion.AngleAxis(_angleFOV, this.transform.up);
        var rotationMod2 = Quaternion.AngleAxis(-_angleFOV, this.transform.up);
        var direction = rotation * rotationMod * Vector3.forward;
        var direction2 = rotation * rotationMod2 * Vector3.forward;
        
        
        var ray = new Ray(transform.position, direction);
        var ray2 = new Ray(transform.position, direction2);
        
        Debug.DrawRay(transform.position, direction * 1.2f, Color.red);
        Debug.DrawRay(transform.position, direction2 * 1.2f, Color.red);
        Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.red);
        
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1.5f, _detectLayer))
        {
            _isMove = false;
            _rigidbody.isKinematic = true;

            if(_aiAttack != null)
                _aiAttack.OnAttack();
        }
        else
        {
            _isMove = true;
            _rigidbody.isKinematic = false;
        }
        
        if(_isMove)
        {
            if (Physics.Raycast(ReturnRaycastDirection(direction), out hit, 1.2f))
            {
                RotationFov(-_angleRotation);
            }

            if (Physics.Raycast(ReturnRaycastDirection(direction2), out hit, 1.2f))
            {
                RotationFov(_angleRotation);
            }
        }

        
        
        
    }

    private void FixedUpdate()
    {
        RotationInTarget(_targetPosition);
        
        if(_isMove)
        {
            _rigidbody.velocity = transform.forward * _walkSpeed * Time.fixedDeltaTime;
        }
    }

    private Ray ReturnRaycastDirection(Vector3 direction)
    {
        var ray = new Ray(transform.position, direction);

        return ray;
    }

    private void RotationFov(float angle)
    {
        Quaternion rotY = Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.up);
        transform.rotation = transform.rotation * rotY;
    }

    private void RotationInTarget(Transform target)
    {
        var direction = target.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
    }
}
