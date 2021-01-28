using UnityEngine;
using System.Collections;
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

        public void Play()
        {
            particles.Play();
        }

        internal void FlipXAxis(bool facingLeft)
        {
            if (facingLeft)
                particlesRenderer.flip = new Vector3(1f, 0f, 0f);
            else
                particlesRenderer.flip = new Vector3(0, 0f, 0f);
        }
    }
}