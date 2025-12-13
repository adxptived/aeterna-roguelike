using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Скорость передвижения")]
    public float moveSpeed = 5f;

    [Tooltip("Ускорение (плавность)")]
    public float acceleration = 50f;

    [Tooltip("Замедление при остановке")]
    public float deceleration = 50f;

    [Header("References")]
    public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Vector2 inputDirection;
    private Vector2 currentVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GatherInput();
        UpdateSpriteDirection();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void GatherInput()
    {
        inputDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        // Нормализуем диагонали
        if (inputDirection.sqrMagnitude > 1f)
            inputDirection.Normalize();
    }

    private void ApplyMovement()
    {
        Vector2 targetVelocity = inputDirection * moveSpeed;

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            currentVelocity = Vector2.MoveTowards(
                currentVelocity,
                targetVelocity,
                acceleration * Time.fixedDeltaTime
            );
        }
        else
        {
            currentVelocity = Vector2.MoveTowards(
                currentVelocity,
                Vector2.zero,
                deceleration * Time.fixedDeltaTime
            );
        }

        rb.linearVelocity = currentVelocity;
    }

    private void UpdateSpriteDirection()
    {
        if (spriteRenderer == null) return;

        if (inputDirection.x < -0.1f)
            spriteRenderer.flipX = true;
        else if (inputDirection.x > 0.1f)
            spriteRenderer.flipX = false;
    }

    public Vector2 GetVelocity() => currentVelocity;
    public Vector2 GetMoveDirection() => inputDirection;
    public bool IsMoving() => currentVelocity.sqrMagnitude > 0.1f;
}
