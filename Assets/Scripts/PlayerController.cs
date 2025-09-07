using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public float smoothTime = 0.1f; // Smoothing time for acceleration/deceleration
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Slider slider;
    public bool playerDie;
    public bool isPlayerWin;
    private CharacterController controller;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 verticalVelocity = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (playerDie || isPlayerWin) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        if (inputDir.magnitude > 0.1f)
        {
            // Smooth movement
            Vector3 targetVelocity = inputDir * moveSpeed;
            currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, 1 - Mathf.Exp(-Time.deltaTime / smoothTime));

            // Rotate towards movement direction
            Quaternion toRotation = Quaternion.LookRotation(inputDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.zero;
        }

        // Jumping and gravity
        if (controller.isGrounded)
        {
            verticalVelocity.y = -1f; // Small downward force to keep grounded
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            verticalVelocity.y += gravity * Time.deltaTime;
        }

        // Move the player (horizontal + vertical)
        Vector3 totalVelocity = currentVelocity;
        totalVelocity.y = verticalVelocity.y;
        controller.Move(totalVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.transform.CompareTag("Bullet"))
        {
            ApplyDamage(0.1f); // bullet deals 0.1 damage
        }
    }

    // Centralized damage handling
    private void ApplyDamage(float amount)
    {
        if (playerDie) return;

        slider.value -= amount;
        Event_Maneger.Trigger("PlayerHealth", slider.value); //HealthUpdate(slider.value);

        if (slider.value <= 0)
        {
            slider.value = 0;
            playerDie = true;
            Event_Maneger.Trigger("LevelFailCall");
        }
    }

    // For enemy melee attacks or AI damage
    public void EnemyAttack()
    {
        ApplyDamage(0.2f); // enemy melee deals 0.2 damage
    }
}
