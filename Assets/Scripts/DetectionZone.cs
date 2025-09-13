using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    // Sự kiện được gọi khi không còn collider nào trong vùng
    public UnityEvent noCollidersRemain;

    // Danh sách các collider đang nằm trong vùng phát hiện
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    // Collider của chính DetectionZone (trigger)
    Collider2D col;

    private void Awake()
    {
        // Lấy collider gắn trên object này (phải bật IsTrigger)
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Khi một collider đi vào vùng → thêm nó vào danh sách
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Khi collider rời khỏi vùng → loại nó khỏi danh sách
        detectedColliders.Remove(collision);

        // Nếu danh sách rỗng (không còn ai trong vùng) → gọi event
        if (detectedColliders.Count <= 0)
        { noCollidersRemain.Invoke(); }
    }
}