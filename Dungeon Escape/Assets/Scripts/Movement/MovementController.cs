using Dungeon.Animation;
using UnityEngine;

namespace Dungeon.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private LayerMask surfaceLayer;
        [SerializeField] private float extraGroundedDistance = 0.1f;

        private bool IsGrounded => CheckIfGrounded();

        private Rigidbody2D _rigidBody;
        private Collider2D _collider2d;
        private CharacterAnimator _animator;

        public void Update()
        {
            // DrawDebugJumping();
        }

        private void Awake()
        {
            _animator = GetComponent<CharacterAnimator>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider2d = GetComponent<Collider2D>();
        }

        /// <summary>
        /// Move method that should be called in FixedUpdate.
        /// Multiplies horizontalInput by moveSpeed and Time.fixedDeltaTime:
        /// horizontalInput * moveSpeed * Time.fixedDeltaTime.
        /// </summary>
        /// <param name="horizontalInput"></param>
        /// <param name="moveSpeed"></param>
        public void Move(float horizontalInput, float moveSpeed)
        {
            _animator.AnimateMovement(horizontalInput, IsGrounded);
            _rigidBody.velocity = new Vector2(horizontalInput * moveSpeed * Time.fixedDeltaTime,
                _rigidBody.velocity.y);
        }

        /// <summary>
        /// Jump method that should be called in FixedUpdate.
        /// Takes a bool (taken from Update) and if true,
        /// changes Y velocity to the new jumpForce velocity: 
        /// jumpForce * Time.fixedDeltaTime
        /// </summary>
        /// <param name="shouldJump"></param>
        /// <param name="jumpForce"></param>
        public void Jump(bool shouldJump, float jumpForce)
        {
            if (shouldJump && IsGrounded)
            {
                _animator.AnimateJumping();
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce * Time.fixedDeltaTime);
            }
        }

        /// <summary>
        /// Stops current movement by assigning a zero Vector to the velocity
        /// And assigning 0 to an animator's horizontal.
        /// </summary>
        public void Stop()
        {
            _rigidBody.velocity = Vector2.zero;
            _animator.Stop();
        }

        private bool CheckIfGrounded()
        {
            RaycastHit2D hit = CastBox();
            return hit.collider; //more efficient null check
        }

        private RaycastHit2D CastBox()
        {
            return Physics2D.BoxCast(_collider2d.bounds.center, _collider2d.bounds.size, 0f,
                Vector2.down, extraGroundedDistance, surfaceLayer);
        }

        public void DrawDebugJumping()
        {
            Color collideColor = (CheckIfGrounded()) ? Color.green : Color.red;
            var bounds = _collider2d.bounds;

            // right
            Debug.DrawRay(bounds.center + new Vector3(bounds.extents.x, 0f), Vector2.down * (bounds.extents.y + extraGroundedDistance), collideColor);
            //left
            Debug.DrawRay(_collider2d.bounds.center - new Vector3(bounds.extents.x, 0f), Vector2.down * (bounds.extents.y + extraGroundedDistance), collideColor);
            //bottom
            Debug.DrawRay(_collider2d.bounds.center - new Vector3(bounds.extents.x, bounds.extents.y), Vector2.right * (bounds.extents.x), collideColor);
        }
    }
}
