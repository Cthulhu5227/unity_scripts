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

        // Create a texture for the selection box
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

            // Create a rectangle from the start point to the end point
            selectionRect = new Rect(startPoint.x, Screen.height - startPoint.y, width, -height);

            // Draw the selection box
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
                // Implement your logic for selecting the player
                Debug.Log("Player selected: " + player.name);
            }
        }
    }
}