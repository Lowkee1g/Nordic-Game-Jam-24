using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySight : MonoBehaviour
{
    public float viewAngle = 90f;
    public float viewDistance = 5f;
    public int rayCount = 100;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public Transform viewPoint;

    public gamemanager gameManager;

    // reference to the slider that will represent the suspicion meter
    public Slider susMeterSlider;

    private bool playerSpotted = false;
    public float susMeter = 0f;
    public float suspicionIncreaseRate = 1f; // Rate at which suspicion meter increases per second

    // Optional: Reference to the enemy's movement script
    public MonoBehaviour enemyMovementScript;
    private void Awake() {
        susMeterSlider.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<gamemanager>();
    }

    private void Update()
    {
        DrawFieldOfView();
        if (playerSpotted)
        {
            IncreaseSusMeter();
        }
    }

    private void DrawFieldOfView()
    {
        bool playerInSight = false;
        float stepAngleSize = viewAngle / rayCount;
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.z - (viewAngle / 2) + stepAngleSize * i;
            Vector3 direction = DirFromAngle(angle, true);
            RaycastHit2D hit = Physics2D.Raycast(viewPoint.position, direction, viewDistance, obstructionMask);

            if (hit.collider == null)
            {
                Debug.DrawRay(viewPoint.position, direction * viewDistance, Color.green);
            }
            else
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.DrawRay(viewPoint.position, direction * hit.distance, Color.yellow);
                    Debug.Log("Player in sight!");
                    playerInSight = true;
                    playerSpotted = true;
                    // Freeze the enemy in place
                    if (enemyMovementScript != null)
                    {
                        Debug.Log("Freezing enemy movement!");
                        enemyMovementScript.enabled = false;
                    }
                }
                else
                {
                    Debug.DrawRay(viewPoint.position, direction * hit.distance, Color.red);
                }
            }
        }

        // If the player was not seen during this frame, set playerSpotted to false
        if (!playerInSight && playerSpotted)
        {
            playerSpotted = false;
            // Optionally, unfreeze the enemy movement if the player is no longer in sight
            if (enemyMovementScript != null)
            {
                enemyMovementScript.enabled = true;
            }
        }
    }

    private void IncreaseSusMeter()
    {
        susMeter += suspicionIncreaseRate * Time.deltaTime;
        susMeter = Mathf.Clamp(susMeter, 0, 1000);
        susMeterSlider.value = susMeter;
        // if the susMeter is higher than 1
        if (susMeter > 1)
        {
            susMeterSlider.gameObject.SetActive(true);
            // decrease the susMeter by 1 every second
            susMeter -= 50 * Time.deltaTime;
        }

        // Here you can implement what happens when the susMeter reaches 1000
        if (susMeter >= 999)
        {
            Debug.Log("Player was spotted by the enemy!");
            gameManager.KillPlayer("You were spotted by the enemy!");
            // find the GameManager object and call the killplayer method

        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }
}
