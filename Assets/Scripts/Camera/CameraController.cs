using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Перетащи сюда объект Player")]
    public Transform target;

    [Header("Follow Settings")]
    [Tooltip("Насколько быстро камера следует за игроком")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;

    [Tooltip("Смещение камеры от игрока")]
    public Vector3 offset = new Vector3(0, 2, -10);

    [Header("Look Ahead (Hades Style)")]
    [Tooltip("Камера смотрит вперёд по направлению движения")]
    public bool enableLookAhead = true;

    [Tooltip("Дистанция опережения")]
    public float lookAheadDistance = 2f;

    [Tooltip("Скорость опережения")]
    public float lookAheadSpeed = 0.1f;

    [Header("Dead Zone")]
    [Tooltip("Зона, в которой камера не двигается")]
    public Vector2 deadZone = new Vector2(0.5f, 0.5f);

    [Header("Bounds (Optional)")]
    public bool useBounds = false;
    public Vector2 minBounds;
    public Vector2 maxBounds;

     
    private const float PPU = 32f;


    // Приватные переменные
    private Vector3 currentVelocity;
    private Vector3 lookAheadPos;
    private PlayerMovement playerMovement;

    private void Start()
    {
        if (target == null)
        {
            // Пытаемся найти игрока автоматически
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("Camera target not set and no Player found!");
                return;
            }
        }

        // Получаем компонент движения для look ahead
        playerMovement = target.GetComponent<PlayerMovement>();

        // Устанавливаем начальную позицию
        transform.position = target.position + offset;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        FollowTarget();
        
    }

    private void FollowTarget()
    {
        // Базовая позиция - позиция игрока + смещение
        Vector3 desiredPosition = target.position + offset;

        // Look Ahead - камера смотрит вперёд
        if (enableLookAhead && playerMovement != null)
        {
            Vector2 moveDir = playerMovement.GetMoveDirection();

            if (moveDir.magnitude > 0.1f)
            {
                // Плавно двигаем точку опережения
                Vector3 targetLookAhead = new Vector3(
                    moveDir.x * lookAheadDistance,
                    moveDir.y * lookAheadDistance * 0.5f, // Меньше по Y для изометрии
                    0
                );

                lookAheadPos = Vector3.Lerp(lookAheadPos, targetLookAhead, lookAheadSpeed);
            }
            else
            {
                // Возвращаем к центру когда не двигаемся
                lookAheadPos = Vector3.Lerp(lookAheadPos, Vector3.zero, lookAheadSpeed);
            }

            desiredPosition += lookAheadPos;
        }

        // Dead Zone - не двигаем камеру если игрок в мёртвой зоне
        Vector3 diff = desiredPosition - transform.position;

        if (Mathf.Abs(diff.x) < deadZone.x)
        {
            desiredPosition.x = transform.position.x;
        }

        if (Mathf.Abs(diff.y) < deadZone.y)
        {
            desiredPosition.y = transform.position.y;
        }

        // Плавное следование
        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref currentVelocity,
            smoothSpeed
        );

        // Применяем границы если нужно
        if (useBounds)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);
        }

        

        // Сохраняем Z позицию
        smoothedPosition.z = offset.z;

        transform.position = smoothedPosition;
    }

    /// <summary>
    /// Мгновенно переместить камеру к цели
    /// </summary>
    public void SnapToTarget()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }

    /// <summary>
    /// Тряска камеры (для эффектов)
    /// </summary>
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private System.Collections.IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position += new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    // Визуализация в редакторе
    private void OnDrawGizmosSelected()
    {
        // Dead Zone
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(deadZone.x * 2, deadZone.y * 2, 0));

        // Bounds
        if (useBounds)
        {
            Gizmos.color = Color.red;
            Vector3 center = new Vector3(
                (minBounds.x + maxBounds.x) / 2,
                (minBounds.y + maxBounds.y) / 2,
                0
            );
            Vector3 size = new Vector3(
                maxBounds.x - minBounds.x,
                maxBounds.y - minBounds.y,
                0
            );
            Gizmos.DrawWireCube(center, size);
        }
    }
   


}