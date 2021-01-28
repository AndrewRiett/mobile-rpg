using Dungeon.Animation;
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
        private CharacterAnimationController characterAnimator;
        private FightingController fighting;

        private float inputHorizontal;

        private bool shouldAttack;
        private bool shouldJump;
        // private bool isGrounded;

        private void Awake()
        {
            movement = GetComponent<MovementController>();
            characterAnimator = GetComponent<CharacterAnimationController>();
            fighting = GetComponent<FightingController>();
        }

        private void Update()
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            shouldJump = Input.GetButton("Jump");
            shouldAttack = Input.GetButtonDown("Fire1");
        }


        private void FixedUpdate()
        {
            UpdateAnimator();

            movement.Jump(shouldJump, jumpForce); 
            movement.Move(inputHorizontal, speed);

            fighting.Attack(shouldAttack);

        }

        private void UpdateAnimator()
        {
            characterAnimator.AnimateMovement(inputHorizontal, movement.IsGrounded, shouldJump);
        }
    }
}

