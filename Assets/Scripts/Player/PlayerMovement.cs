using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;
    [SerializeField] Vector2 minBoundary;
    [SerializeField] Vector2 maxBoundary;

    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / new Vector2(Mathf.Pow(timeToFullSpeed.x, 2), Mathf.Pow(timeToFullSpeed.y, 2));
        stopFriction = -2 * maxSpeed / new Vector2(Mathf.Pow(timeToStop.x, 2), Mathf.Pow(timeToStop.y, 2));
    }

    void Update()
    {
        Move();
        ApplyFriction();
        //MoveBound();
    }

    public void Move()
    {
        float xVelocity = moveVelocity.x;
        float yVelocity = moveVelocity.y;

        float horizontalTranslation = Input.GetAxis("Horizontal") * xVelocity;
        float verticalTranslation = Input.GetAxis("Vertical") * yVelocity;

        horizontalTranslation *= Time.deltaTime;
        verticalTranslation *= Time.deltaTime;

        moveDirection = new Vector2(horizontalTranslation, verticalTranslation);

        transform.Translate(horizontalTranslation, verticalTranslation, 0);
    }

    void ApplyFriction()
    {
        if (moveDirection != Vector2.zero)
        {
            // Apply move friction
            rb.linearVelocity += (moveVelocity + moveFriction) * moveDirection * Time.deltaTime;
        }
        else
        {
            // Apply stop friction
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, stopFriction.x);
        }
    }

    void MoveBound()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBoundary.x, maxBoundary.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBoundary.y, maxBoundary.y);
        transform.position = clampedPosition;
    }

    public bool isMoving()
    {
        return moveDirection != Vector2.zero;
    }
}
