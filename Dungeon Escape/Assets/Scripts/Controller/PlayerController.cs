using Dungeoun.Movement;
using UnityEngine;

namespace Dungeoun.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 0f;
        [SerializeField] private float jumpForce = 0f;

        private MovementController movement;
        private float inputHorizontal;
        private bool shouldJump;

        private void Awake()
        {
            movement = GetComponent<MovementController>();
        }

        private void Update()
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            shouldJump = Input.GetButtonDown("Jump");
        }

        private void FixedUpdate()
        {
            movement.Move(inputHorizontal, speed);
            movement.Jump(shouldJump, jumpForce); 
        }
    }
}

