using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerToCheck;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashTime;


    [SerializeField]
    private Rigidbody2D playerRb;
    [SerializeField]
    private Transform groundPoint;

    private bool isOnGround;
    private bool isDoubleJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        playerRb.linearVelocity = new Vector2(xAxis * moveSpeed, playerRb.linearVelocity.y);
        if (playerRb.linearVelocityX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (playerRb.linearVelocityX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //kiểm tra chạm đất
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, 0.2f, layerToCheck);
        // Debug.Log($"{isOnGround}");

        // nhảy
        if (Input.GetButtonDown("Jump") && (isOnGround || (isDoubleJump)))
        {
            if (isOnGround)
            {
                isDoubleJump = true;
            }
            else
            {

                isDoubleJump = false;
            }
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);
        }

    }
}
