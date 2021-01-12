using Dungeon.Movement;
using UnityEngine;

namespace Dungeon.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 0f;
        [SerializeField] private float jumpForce = 0f;

        private MovementController movement;
        private AnimationController animating;

        private float inputHorizontal;
        private bool shouldJump;

        private void Awake()
        {
            movement = GetComponent<MovementController>();
            animating = GetComponent<AnimationController>();
        }

        private void Update()
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            shouldJump = Input.GetButtonDown("Jump");

            animating.AnimateMovement(inputHorizontal);
        }

        private void FixedUpdate()
        {
            movement.Jump(shouldJump, jumpForce); 
            movement.Move(inputHorizontal, speed);
        }
    }
}

