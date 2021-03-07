using UnityEngine;
using System;

namespace Dungeon.Animation
{
    public class VFXAnimationController : MonoBehaviour
    {
        private ParticleSystem particles;
        private ParticleSystemRenderer particlesRenderer;

        private void Awake()
        {
            particles = GetComponent<ParticleSystem>();
            particlesRenderer = GetComponent<ParticleSystemRenderer>();
        }

        private void OnEnable()
        {
            CharacterAnimator.OnFlip += FlipXAxis;
        }

        public void Play()
        {
            particles.Play();
        }

        // BUG: doesn't flip the axis sometimes (apply horizontal instead?)
        private void FlipXAxis(bool facingLeft)
        {
            if (facingLeft)
                particlesRenderer.flip = new Vector3(1f, 0f, 0f);
            else
                particlesRenderer.flip = new Vector3(0, 0f, 0f);
        }
    }
}