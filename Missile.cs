using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private float speed;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject particle;

    private float currentSpeed;

    private IEnumerator Start()
    {
        SearchTarget();

        while (true)
        {
            if (target == null)
            {
                DestroyThis();

                yield break;
            }

            if (currentSpeed <= speed)
            {
                currentSpeed += speed * Time.deltaTime;
            }

            transform.position += transform.forward * currentSpeed * Time.deltaTime;

            Vector3 direction = (target.position - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, direction, 0.25f);

            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            DestroyThis();
        }
    }

    private void SearchTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, 100f, layerMask);

        if (targets.Length > 0)
        {
            target = targets[Random.Range(0, targets.Length)].transform;
        }
    }

    private void DestroyThis()
    {
        Destroy(Instantiate(particle, transform.position, Quaternion.identity), 0.7f);
        Destroy(gameObject);
    }
}
