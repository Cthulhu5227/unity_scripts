using UnityEngine;

public class selectoController : MonoBehaviour
{
    private Vector2 startPoint;
    private Rect selectionRect;
    private bool isSelecting = false;
    public Color selectionColor = Color.green;
    private Texture2D selectionTexture;
    private GameObject[] players;

    void Start()
    {
        // Find all player GameObjects
        players = GameObject.FindGameObjectsWithTag("Player");

        //  texture for the selection box
        selectionTexture = new Texture2D(1, 1);
        selectionTexture.SetPixel(0, 0, selectionColor);
        selectionTexture.Apply();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            startPoint = Input.mousePosition;
            isSelecting = true;
        }

        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            isSelecting = false;
            SelectPlayersInRectangle();
        }
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            Vector2 endPoint = Event.current.mousePosition;
            float width = endPoint.x - startPoint.x;
            float height = endPoint.y - startPoint.y;

            selectionRect = new Rect(
                Mathf.Min(startPoint.x, endPoint.x),
                Screen.height - Mathf.Max(startPoint.y, endPoint.y),
                Mathf.Abs(-width),
                Mathf.Abs(height)
            );

            // draw the  box
            GUI.color = selectionColor;
            GUI.DrawTexture(selectionRect, selectionTexture);
        }
    }

    void SelectPlayersInRectangle()
    {
        foreach (GameObject player in players)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(player.transform.position);
            if (selectionRect.Contains(screenPosition))
            {
                // Ensure the player has a component that can mark it as selected
                PlayerController playerController = player.GetComponent<PlayerController>();
                SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
                if (playerController != null)
                {
                    playerController.isSelected = true;
                    spriteRenderer.color = Color.red;
                }
            }
        }
    }
}