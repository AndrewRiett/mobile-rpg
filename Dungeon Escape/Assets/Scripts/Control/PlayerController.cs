using Dungeon.Fighting;
using Dungeon.Movement;
using UnityEngine;

namespace Dungeon.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 0f;
        [SerializeField] private float jumpForce = 0f;

        private MovementController _movement;
        private FightingController _fighting;

        private float _inputHorizontal;

        private bool _shouldAttack;
        private bool _shouldJump;
        // private bool isGrounded;

        private void Awake()
        {
            _movement = GetComponent<MovementController>();
            _fighting = GetComponent<FightingController>();
        }

        private void Update()
        {
            _inputHorizontal = Input.GetAxisRaw("Horizontal");
            _shouldJump = Input.GetButton("Jump");
            _shouldAttack = Input.GetButtonDown("Fire1");
        }


        private void FixedUpdate()
        {
            _movement.Jump(_shouldJump, jumpForce); 
            _movement.Move(_inputHorizontal, speed);

            _fighting.Hit(_shouldAttack);
        }
    }
}

