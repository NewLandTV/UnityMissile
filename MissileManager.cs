using System.Collections;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    [SerializeField]
    private Missile missilePrefab;

    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnMissile();
            }

            yield return null;
        }
    }

    private void SpawnMissile()
    {
        Destroy(Instantiate(missilePrefab, Vector3.up * 60f, Quaternion.identity, transform), 10f);
    }
}
