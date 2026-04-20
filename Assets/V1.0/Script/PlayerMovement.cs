using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveDistance = 5f;
    [SerializeField] float moveSpeed = 10f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 7f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    [Header("Slide")]
    [SerializeField] float slideDuration = 1.533f;
    [SerializeField] float slideColliderHeight = 0.5f;

    Rigidbody rb;
    Animator animator;
    BoxCollider col;

    Vector3 targetPos;
    Vector3 originalSize;
    Vector3 originalCenter;

    float slideTimer;
    bool moving;
    bool grounded;

    static readonly int Jump = Animator.StringToHash("Jump");
    static readonly int Slide = Animator.StringToHash("Slide");

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        if (col != null)
        {
            originalSize = col.size;
            originalCenter = col.center;
        }
    }

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        HandleSlideCollider();
    }

    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);

        if (!moving) return;

        Vector3 pos = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(pos);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            transform.position = targetPos;
            moving = false;
        }
    }

    void HandleSlideCollider()
    {
        if (slideTimer <= 0) return;

        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0 && col != null)
        {
            col.size = originalSize;
            col.center = originalCenter;
        }
    }

    public void OnMoveLeft(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || moving) return;
        targetPos = transform.position + Vector3.left * moveDistance;
        moving = true;
    }

    public void OnMoveRight(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || moving) return;
        targetPos = transform.position + Vector3.right * moveDistance;
        moving = true;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!ctx.started || !grounded) return;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger(Jump);
    }

    public void OnSlide(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !grounded) return;

        animator.SetTrigger(Slide);

        if (col != null)
        {
            col.size = new Vector3(originalSize.x, slideColliderHeight, originalSize.z);
            col.center = new Vector3(
                originalCenter.x,
                originalCenter.y - (originalSize.y - slideColliderHeight) / 2f,
                originalCenter.z
            );
        }

        slideTimer = slideDuration;
    }

    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}