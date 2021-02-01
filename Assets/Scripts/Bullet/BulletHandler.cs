using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _xRejection = 0;
    [SerializeField] private float _minBulletMoveSpeed = 30f;
    [SerializeField] private float _maxBulletMoveSpeed = 30f;

    private void OnBecameVisible()
    {
        StartCoroutine(TimeToInvise(1f));
        transform.Rotate(new Vector3(0, Random.Range(-_xRejection, _xRejection), 0));
    }


    private void Update()
    {
        transform.Translate(Vector3.forward * Random.Range(_minBulletMoveSpeed, _maxBulletMoveSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            var health = other.gameObject.GetComponent<MainHealthHandler>();

            if (health != null)
            {
                health.HandleDamage(_damage, transform);
            }
        }

        gameObject.SetActive(false);
    }

    private IEnumerator TimeToInvise(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

}
