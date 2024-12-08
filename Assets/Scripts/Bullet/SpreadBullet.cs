using System.Collections;
using UnityEngine;

namespace Bullet
{
    public class SpreadBullet : MonoBehaviour
    {
        [SerializeField] private GameObject[] _bullets = new GameObject[20];

        private void OnEnable() 
        {
            StartCoroutine(TimeToInvise(1.2f));
            for (var i = 0; i < _bullets.Length; i++)
            {
                _bullets[i].transform.position = transform.position;
                _bullets[i].transform.rotation = transform.rotation;
                _bullets[i].SetActive(true);
            }
        }

        private IEnumerator TimeToInvise(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }
    }
}
