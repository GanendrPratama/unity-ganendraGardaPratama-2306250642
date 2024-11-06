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

    public void Move()
    {
        float xVelocity = moveVelocity.x;
        float yVelocity = moveVelocity.y;

        float horizontalTranslation = Input.GetAxis("Horizontal") * xVelocity;
        float verticalTranslation = Input.GetAxis("Vertical") * yVelocity;

        horizontalTranslation *= Time.fixedDeltaTime;
        verticalTranslation *= Time.fixedDeltaTime;

        moveDirection = new Vector2(horizontalTranslation, verticalTranslation).normalized;

        rb.linearVelocity = (moveVelocity + ApplyFriction()) * moveDirection;
    }

    public Vector2 ApplyFriction()
    {
        if (moveDirection != Vector2.zero)
        {
            // Apply move friction
            return moveFriction;
        }
        else
        {
            // Apply stop friction
            return stopFriction;
        }
    }

    public void MoveBound()
    {
        Vector3 screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, screenBound.x * -1, screenBound.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, screenBound.y * -1, maxBoundary.y);
        transform.position = clampedPosition;
    }

    public bool IsMoving()
    {
        Debug.Log(moveDirection.x + " " + moveDirection.y);
        return moveDirection != Vector2.zero;
    }
}
