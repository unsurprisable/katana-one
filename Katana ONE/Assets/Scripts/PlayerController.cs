using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float walkForce = 5f;
    [SerializeField] private float sprintActivationSpeed = 1f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private bool isSprinting;
    private Vector2 inputDir;

    [Header("Physics")]
    [SerializeField] private bool isGrounded; // todo

    [Header("Audio")]
    [SerializeField] private AudioClip sprintSFX;
    private bool sprintSFXCanPlay = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputDir = Vector2.zero;
    }

    private void FixedUpdate()
    {

        inputDir = new Vector2(GameInput.Instance.GetHorizontalMovement(), GameInput.Instance.GetVerticalMovement());
        
        if (inputDir.x != 0 && ((rb.linearVelocityX < 0f && inputDir.x > 0f) || (rb.linearVelocityX > 0f && inputDir.x < 0f))) {
            SetLinearVelocity(0f);
            isSprinting = false;
            Debug.Log("quickstop");
        }
        
        if (rb.linearVelocityX == 0) {
            sprintSFXCanPlay = true;
        }
        
        if (inputDir.x == 0)
        {
            isSprinting = false;
        }

        if (!isSprinting && inputDir.x != 0) {
            rb.AddForceX(walkForce * inputDir.x);
        }

        // if horz velocity direction matches input direction
        if ((rb.linearVelocityX < 0 && inputDir.x < 0) || (rb.linearVelocityX > 0 && inputDir.x > 0)) {
            if (Mathf.Abs(rb.linearVelocityX) >= sprintActivationSpeed) {
                SetLinearVelocity(sprintSpeed * inputDir.x);
                if (sprintSFXCanPlay && !isSprinting) {
                    AudioManager.Instance.PlaySoundClip(sprintSFX, transform.position);
                    sprintSFXCanPlay = false;
                }
                isSprinting = true;
            } 
        }
    }

    private Vector2 SetLinearVelocity(float x) {
        rb.linearVelocity = new Vector2(x, rb.linearVelocityY);
        return rb.linearVelocity;
    }
    private Vector2 SetLinearVelocity(float x, float y) {
        rb.linearVelocity = new Vector2(x, y);
        return rb.linearVelocity;
    }
}
