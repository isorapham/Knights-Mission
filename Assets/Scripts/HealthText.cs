using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0); // Tốc độ di chuyển của text (mặc định bay lên trên trục Y)
    public float timeToFade = 1f;                   // Thời gian text sẽ dần biến mất (fade out) tính bằng giây

    RectTransform textTransform;    // Lưu tham chiếu tới RectTransform (vị trí/scale của UI text)
    TextMeshProUGUI textMeshPro;    // Lưu tham chiếu tới component TextMeshProUGUI (để đổi màu, chữ,...)

    private float timeElapsed = 0f; // Biến đếm thời gian text đã tồn tại
    private Color startColor;       // Màu ban đầu của text (gồm cả alpha)

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();   // Lấy component RectTransform gắn trên object
        textMeshPro = GetComponent<TextMeshProUGUI>();   // Lấy component TextMeshProUGUI gắn trên object
        startColor = textMeshPro.color;                  // Lưu lại màu ban đầu (để tính hiệu ứng fade)
    }

    private void Update()
    {
        // Di chuyển text mỗi frame theo hướng moveSpeed, nhân với deltaTime để không phụ thuộc FPS
        textTransform.position += moveSpeed * Time.deltaTime;

        // Tăng thời gian đã trôi qua
        timeElapsed += Time.deltaTime;

        // Nếu chưa vượt quá thời gian fade
        if (timeElapsed < timeToFade)
        {
            // Tính alpha mới: từ 1 → 0 dựa theo tỉ lệ thời gian
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade));

            // Gán lại màu mới với alpha giảm dần
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            // Khi đã vượt quá thời gian fade → xóa object này khỏi scene
            Destroy(gameObject);
        }
    }
}