using UnityEngine;

namespace Dungeon.Animation
{
    public class SwordArcAnimation : ObjectAnimator
    {
        public override void Animate()
        {
            animator.SetTrigger("swordSwing");
        }
    }
}