using UnityEngine;
using Dungeon.Animation;

namespace Dungeon.Navigation
{
    public class SurfaceChecker : MonoBehaviour
    {
        [Header("Obstacles:")]
        [SerializeField] private LayerMask surfaceLayer;
        [SerializeField] private float colliderHighModifier = 0.5f;
        [SerializeField] private float horizontalCheckDistance = 1f;
        [SerializeField] private float groundCheckDistance = 2f;

        private Collider2D _collider;
        private readonly Vector2 _vector180 = new Vector2(0f, 180f); // for not allocating redundant memory each frame
        
        private void Awake()
        {
            _collider = GetComponentInParent<Collider2D>();
        }

        private void OnEnable()
        {
            CharacterAnimator.OnFlip += FlipXAxis;
        }

        private void Update()
        {
            DrawRay();
        }

        // horizontal == 0f -> Vector.zero, else -> 180
        // or 180 * horizontal (?) results: 0/180/-180
        private void FlipXAxis(bool facingLeft)
        {
            transform.localEulerAngles = facingLeft ? 
                _vector180 : Vector2.zero;
        }
        
        // BUG: a ray has a trail
        private void DrawRay() // Vector2 direction, float distance
        {
            Vector2 objectHigh = new Vector3(transform.position.x,
            (_collider.bounds.max.y + colliderHighModifier)); 
            
            Physics2D.Raycast(objectHigh, transform.right, horizontalCheckDistance, surfaceLayer);
            Debug.DrawRay(objectHigh, transform.right, Color.blue, horizontalCheckDistance);
            
            //return false;
        }
    }
}