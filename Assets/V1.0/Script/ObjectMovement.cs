using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float destroyAtZ = -10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation
                       | RigidbodyConstraints.FreezePositionX
                       | RigidbodyConstraints.FreezePositionY;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector3.back * moveSpeed;

        if (transform.position.z < destroyAtZ)
            Destroy(gameObject);
    }
}