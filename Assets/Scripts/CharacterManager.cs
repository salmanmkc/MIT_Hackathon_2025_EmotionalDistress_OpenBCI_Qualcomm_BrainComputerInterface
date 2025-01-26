using System.Collections;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 minBounds; // Minimum x, y, z for the cube volume
    public Vector3 maxBounds; // Maximum x, y, z for the cube volume
    public float moveSpeed = 1.0f; // Speed of the object's movement
    public float pauseDuration = 2.0f; // Time to pause before moving to the next position

    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip[] calmingSounds; // Array of calming audio clips to play randomly

    private Vector3 targetPosition; // The next position to move to

    void Start()
    {
        // Initialize the first target position
        SetNewTargetPosition();
        // Optionally play an initial sound
        PlayRandomSound();
    }

    void Update()
    {
        // Move the object toward the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the object has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            StartCoroutine(ChangePositionAfterPause());
        }
    }

    private void SetNewTargetPosition()
    {
        // Generate a random position within the defined bounds
        float x = Random.Range(minBounds.x, maxBounds.x);
        float y = Random.Range(minBounds.y, maxBounds.y);
        float z = Random.Range(minBounds.z, maxBounds.z);

        targetPosition = new Vector3(x, y, z);
    }

    private IEnumerator ChangePositionAfterPause()
    {
        // Wait for the specified pause duration
        yield return new WaitForSeconds(pauseDuration);

        // Set a new target position
        SetNewTargetPosition();

        // Play a random sound
        PlayRandomSound();
    }

    private void PlayRandomSound()
    {
        if (calmingSounds.Length > 0 && audioSource != null)
        {
            // Choose a random clip from the array and play it
            AudioClip clip = calmingSounds[Random.Range(0, calmingSounds.Length)];
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wireframe cube in the Scene view to visualize the movement bounds
        Gizmos.color = Color.cyan;
        Vector3 center = (minBounds + maxBounds) / 2;
        Vector3 size = maxBounds - minBounds;
        Gizmos.DrawWireCube(center, size);
    }
}