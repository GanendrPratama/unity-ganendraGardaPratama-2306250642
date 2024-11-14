using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [SerializeField]public Transform parentTransform;
    [SerializeField] private Bullet BulletPrefab;

    [SerializeField] private Transform spawnPosition;

    [SerializeField] private float cdWindow = 0.1f;

    private IObjectPool<Bullet> playerBulletObjectPool;

    private bool collectionCheck = true;

    [SerializeField] private int defAmount;

    [SerializeField] private int maxAmount;

    private float nextTimeToShoot;

    private Bullet CreateBullet()
    {
        Bullet instance = Instantiate(BulletPrefab);
        instance.ObjectPool = playerBulletObjectPool;
        return instance;
    }

    private void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Awake()
    {
        playerBulletObjectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool
        , OnDestroy, collectionCheck, defAmount, maxAmount);
    }

    void FixedUpdate()
    {
        if (Time.time > nextTimeToShoot && playerBulletObjectPool != null)
        {
            Bullet bulletObject = playerBulletObjectPool.Get();

            if (bulletObject = null)
            {
                return;
            }

            bulletObject.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);

            bulletObject.GetComponent<Rigidbody2D>().AddForce(bulletObject.transform.forward * bulletObject.bulletSpeed, ForceMode2D.Force);

            bulletObject.Deactivate(cdWindow);

            nextTimeToShoot = Time.time + cdWindow;
        }
    }
}