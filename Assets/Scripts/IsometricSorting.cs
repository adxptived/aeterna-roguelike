using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IsometricSorting : MonoBehaviour
{
    [Tooltip("Смещение сортировки")]
    public int sortingOffset = 0;
    
    [Tooltip("Обновлять каждый кадр (для движущихся объектов)")]
    public bool updateEveryFrame = true;
    
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void LateUpdate()
    {
        if (updateEveryFrame)
        {
            UpdateSortingOrder();
        }
    }
    
    public void UpdateSortingOrder()
    {
        // В изометрии: чем ниже объект - тем позже он рисуется
        // Умножаем на -100 для точности
        spriteRenderer.sortingOrder = (int)(-(transform.position.y * 100)) + sortingOffset;
    }
}