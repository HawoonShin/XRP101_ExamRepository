using System.Collections;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private CustomObjectPool _bulletPool;
    [SerializeField] private float _fireCooltime;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        Init();
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거 입장");
        if (other.CompareTag("Player"))
        {
            Fire(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.position.x > Mathf.Abs(10)
            || other.transform.position.z > Mathf.Abs(10))
        {
            Debug.Log("코루틴 스탑");
            StopAllCoroutines();
        }
        else return;
    }

    private void Init()
    {
        _coroutine = null;
        _wait = new WaitForSeconds(_fireCooltime);
        _bulletPool.CreatePool();
    }

    private IEnumerator FireRoutine(Transform target)
    {
        while (true)
        {
            yield return _wait;

            // 총알이 나가야 할 방향 지정
            transform.rotation = Quaternion.LookRotation(new Vector3(
                target.position.x,
                0,
                target.position.z)
            );

            PooledBehaviour bullet = _bulletPool.TakeFromPool();
            bullet.transform.position = _muzzlePoint.position;
            bullet.OnTaken(target);

        }
    }

    private void Fire(Transform target)
    {
        _coroutine = StartCoroutine(FireRoutine(target));
    }
}
