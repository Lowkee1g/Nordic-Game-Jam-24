using UnityEngine;

public class SpritePlacer : MonoBehaviour
{
    public GameObject spritePrefab; // Assign your sprite prefab here
    public LayerMask placementLayerMask; // Set this to the layer(s) that mirrors can be placed on
    private GameObject currentSpriteInstance;
    public Camera mainCamera;
    private float currentRotation;
    public float placementDepth = 5f; // Depth at which the sprites will be placed
    private Color placeableColor = Color.cyan; // Color when placement is allowed
    private Color nonPlaceableColor = Color.red; // Color when placement is not allowed

    public float maxDis = 10;

    // Safe the original color of the sprite
    private Color originalColor;

    public int maxSpejles;

    public int currentSpejles;

    private GameObject playerObject;

    void Start()
    {
        originalColor = spritePrefab.GetComponent<SpriteRenderer>().color;

        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private float NormalizeAngle(float angle)
{
    // Normalize the angle to be within 0 and 360 degrees
    angle = angle % 360f;
    if (angle < 0)
    {
        angle += 360f;
    }
    return angle;
}


    void Update()
    {
        // When 'M' key is pressed, create a new sprite at the mouse position
        if (Input.GetKeyDown(KeyCode.M) && currentSpriteInstance == null)
        {
            Vector3 spawnPosition = GetWorldPositionAtDepth(Input.mousePosition, placementDepth);
            currentSpriteInstance = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);
            currentSpriteInstance.GetComponent<Collider2D>().isTrigger = true;
            //currentSpriteInstance.transform.localScale = Vector3.one; // Adjust if your prefab is not 1:1 scale
            currentRotation = 0f;
        } else if (Input.GetKeyDown(KeyCode.M) && currentSpriteInstance != null)
        {
            Destroy(currentSpriteInstance);
            currentSpriteInstance = null;
        }

        // If there's a sprite, follow the mouse position, rotate, and check for placement
        if (currentSpriteInstance != null)
        {

            if (!NearPlayer(this.transform.position))
            {
                Destroy(currentSpriteInstance);
                currentSpriteInstance = null;
                return;
            }
            currentSpriteInstance.transform.position = GetWorldPositionAtDepth(Input.mousePosition, placementDepth);

            // Rotate the sprite by pressing r
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentRotation += 22.5f; // Rotate in increments of 22.5 degrees
                currentRotation = NormalizeAngle(currentRotation); // Keep the angle between 0-360 degrees
                currentSpriteInstance.transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
            }

            // Check mouse scroll wheel and update rotation in increments of 22.5 degrees
            float scroll = Input.mouseScrollDelta.y;
            if (scroll != 0)
            {
                currentRotation += scroll > 0 ? 22.5f : -22.5f; // Rotate in increments of 22.5 degrees
                currentRotation = NormalizeAngle(currentRotation); // Keep the angle between 0-360 degrees
                currentSpriteInstance.transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
            }


            // Check if the mirror's placement is valid
            bool canPlace = !IsCollidingWithWall(currentSpriteInstance) && IsInRange((currentSpriteInstance.transform.position - playerObject.transform.position).magnitude) && hasMoreSpejles();
            SpriteRenderer spriteRenderer = currentSpriteInstance.GetComponent<SpriteRenderer>();
            spriteRenderer.color = canPlace ? placeableColor : nonPlaceableColor;

            // Place sprite with left mouse click if it's not colliding
            if (canPlace && Input.GetMouseButtonDown(0))
            {
                currentSpriteInstance.GetComponent<SpriteRenderer>().color = originalColor; // Reset color or set to a default color
                currentSpriteInstance.GetComponent<Collider2D>().isTrigger = false;
                currentSpriteInstance = null; // Deselect sprite, allowing for a new one to be created
                currentSpejles++;
            }
        }
    }

    private bool IsCollidingWithWall(GameObject spriteInstance)
    {
        Collider2D collider = spriteInstance.GetComponent<Collider2D>();
        if (collider != null)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(collider.bounds.center, collider.bounds.size, 0f, placementLayerMask);
            foreach (Collider2D hit in hits)
            {
                // If any of the colliders we overlapped has the tag "Wall", return true
                if (hit.gameObject.CompareTag("Wall"))
                {
                    Debug.Log("Colliding with wall!");
                    return true;
                }
            }
        }
        return false;
    }

    private bool NearPlayer(Vector3 pos)
    {
        return (pos - playerObject.transform.position).magnitude < maxDis;
    }

    private bool hasMoreSpejles()
    {
        return currentSpejles < maxSpejles;
    }

    private bool IsInRange(float dis)
    {
        //Debug.Log(dis);
        return dis < maxDis;
    }

 
    private Vector3 GetWorldPositionAtDepth(Vector3 screenPosition, float depth)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, depth));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
