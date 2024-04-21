using UnityEngine;

public class SlowmoTrigger : MonoBehaviour
{
    public string targetTag = "Enemy"; // Tag for the objects that trigger slow motion
    public float slowMotionFactor = 0.5f; // The amount to slow down the game

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Time.timeScale = slowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale; // Adjust the fixed delta time according to the time scale
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f; // Reset the fixed delta time
        }
    }
}
