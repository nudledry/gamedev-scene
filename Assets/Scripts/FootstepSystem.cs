using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    [Header("Footstep Settings")]
    public float stepInterval = 0.5f; 
    public float walkSpeedThreshold = 0.1f; 
    
    private float stepTimer;
    private Vector3 lastPosition;
    private CharacterController characterController;
    private Rigidbody rb;
    
    void Start()
    {
        // Cek komponen movement yang digunakan
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }
    
    void Update()
    {
        HandleFootsteps();
    }
    
    void HandleFootsteps()
    {
        float currentSpeed = GetPlayerSpeed();
        
        // Cek apakah player bergerak
        if (currentSpeed > walkSpeedThreshold && IsGrounded())
        {
            stepTimer += Time.deltaTime;
            
            // Adjust step interval based on speed
            float adjustedInterval = stepInterval / (currentSpeed / 2f);
            
            if (stepTimer >= adjustedInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
        
        lastPosition = transform.position;
    }
    
    float GetPlayerSpeed()
    {
        if (characterController != null)
        {
            // Untuk CharacterController
            return characterController.velocity.magnitude;
        }
        else if (rb != null)
        {
            // Untuk Rigidbody
            return rb.linearVelocity.magnitude;
        }
        else
        {
            // Untuk movement manual
            return (transform.position - lastPosition).magnitude / Time.deltaTime;
        }
    }
    
    bool IsGrounded()
    {
        if (characterController != null)
        {
            return characterController.isGrounded;
        }
        else
        {
            // Raycast untuk check ground
            return Physics.Raycast(transform.position, Vector3.down, 1.1f);
        }
    }
    
    void PlayFootstepSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("Footstep");
        }
    }
}