using UnityEngine;

namespace Dungeon.Movement
{
    public class MovementController : MonoBehaviour
    {
        [Header("Jumping:")]
        [SerializeField] private LayerMask surfaceLayer;
        [SerializeField] private float extraGroundedDistance = 0.5f;

        private Rigidbody2D rigidBody;
        private Collider2D collider2d;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            collider2d = GetComponent<Collider2D>();
        }

        private void Update()
        {
            // DrawDebugJumping();
        }

        /// <summary>
        /// Move method that should be called in FixedUpdate.
        /// Multiplies horizontalInput by moveSpeed and Time.fexedDeltaTime:
        /// horizontalInput * moveSpeed * Time.fixedDeltaTime.
        /// </summary>
        /// <param name="horizontalInput"></param>
        /// <param name="moveSpeed"></param>
        public void Move(float horizontalInput, float moveSpeed)
        {
            rigidBody.velocity = new Vector2(horizontalInput * moveSpeed * Time.fixedDeltaTime,
                rigidBody.velocity.y);
        }

        /// <summary>
        /// Jump method that should be called in FixedUpdate.
        /// Takes a bool (taken from Update) and if true,
        /// changes Y velocity to a new jumpForce velocity: 
        /// jumpForce * Time.fixedDeltaTime
        /// </summary>
        /// <param name="shouldJump"></param>
        /// <param name="jumpForce"></param>
        public void Jump(bool shouldJump, float jumpForce)
        {
            if (shouldJump && IsGrounded())
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * Time.fixedDeltaTime);
            }
        }

        public bool IsGrounded()
        {
            RaycastHit2D hit = CastBox();

            return hit.collider != null;
        }

        private RaycastHit2D CastBox()
        {
            return Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f,
                Vector2.down, extraGroundedDistance, surfaceLayer);
        }

        public void DrawDebugJumping()
        {
            RaycastHit2D hit = CastBox();

            Color collideColor;

            if (hit.collider != null)
                collideColor = Color.green;
            else
                collideColor = Color.red;

            Debug.DrawRay(collider2d.bounds.center + new Vector3(collider2d.bounds.extents.x, 0f), Vector2.down * (collider2d.bounds.extents.y + extraGroundedDistance), collideColor);
            Debug.DrawRay(collider2d.bounds.center - new Vector3(collider2d.bounds.extents.x, 0f), Vector2.down * (collider2d.bounds.extents.y + extraGroundedDistance), collideColor);
            Debug.DrawRay(collider2d.bounds.center - new Vector3(collider2d.bounds.extents.x, collider2d.bounds.extents.y), Vector2.right * (collider2d.bounds.extents.x), collideColor);
        }
    }
}
