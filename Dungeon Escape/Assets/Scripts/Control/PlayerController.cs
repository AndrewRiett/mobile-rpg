using Dungeon.Fighting;
using Dungeon.Movement;
using UnityEngine;

namespace Dungeon.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 0f;
        [SerializeField] private float jumpForce = 0f;

        private MovementController movement;
        private AnimationController animating;
        private FightingController fighting;

        private float inputHorizontal;

        private bool shouldAttack;
        private bool shouldJump;
        // private bool isGrounded;

        private void Awake()
        {
            movement = GetComponent<MovementController>();
            animating = GetComponent<AnimationController>();
            fighting = GetComponent<FightingController>();
        }

        private void Update()
        {
            movement.DrawDebugJumping();
            // isGrounded = movement.IsGrounded();

            inputHorizontal = Input.GetAxisRaw("Horizontal");
            shouldJump = Input.GetButton("Jump");
            shouldAttack = Input.GetButtonDown("Fire1");
        }


        private void FixedUpdate()
        {
            movement.Jump(shouldJump, jumpForce); 
            movement.Move(inputHorizontal, speed);

            fighting.Attack(shouldAttack);

            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animating.AnimateMovement(inputHorizontal, movement.IsGrounded, shouldJump);
            animating.AnimateAttack(shouldAttack, movement.IsGrounded);
        }
    }
}

