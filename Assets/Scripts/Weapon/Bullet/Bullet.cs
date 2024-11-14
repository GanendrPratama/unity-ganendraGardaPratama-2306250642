using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20.0f;
    public int damage = 10;
    private Rigidbody2D rb;

    private float TODelay = 3.0f;

    private IObjectPool<Bullet> objectPool;

    public IObjectPool<Bullet> ObjectPool { set => objectPool = value; }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.linearVelocity = new Vector3(0f, 0f, 0f);
        rBody.angularVelocity = new Vector3(0f, 0f, 0f);

        objectPool.Release(this);

    }

    public void Deactivate(float TODelay)
    {
        StartCoroutine(DeactivateRoutine(TODelay));
    }

    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = Vector2.up * bulletSpeed * Time.deltaTime;
    }
}
