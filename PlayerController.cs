using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isMoving;
    public bool isSelected;
    private SpriteRenderer spriteRenderer;

    public Color selectedColor = Color.red; // Color when selected
    private Color originalColor; // Original color of the player

    private void Start()
    {
        // Add Rigidbody2D component if not already present
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        // Configure Rigidbody2D
        rb.gravityScale = 0; // No gravity
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Freeze Z rotation

        // Initialize other components
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Store the original color
        targetPosition = rb.position; // Initialize targetPosition with the player's current position
    }

    private void Update()
    {
        mouseLeftClick();
        mouseRightClick();

    }

    private void mouseLeftClick()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isSelected = true;
                spriteRenderer.color = selectedColor; // Change color when selected
                Debug.Log("Player selected!");
            }
            else if (isSelected)
            {
                targetPosition = new Vector2(mousePosition.x, mousePosition.y);
                isMoving = true;
            }

        }
    }

    // Method to handle right-click for deselection
    private void mouseRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isSelected = false;
            spriteRenderer.color = originalColor; // Revert to original color when deselected
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);

            // Check if the player has reached the target position
            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision with other objects
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Player collided with an obstacle!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handle trigger collisions with other objects
        if (other.gameObject.CompareTag("Item"))
        {
            Debug.Log("Player picked up an item!");
        }
    }
}