using UnityEngine;

namespace Controllers.Animation.Character
{
    public class CharacterAnimationController : MonoBehaviour
    {
        private readonly string[] _mAnimations = { "Pickup", "Wave" };
        private Animator[] _mAnimators;
        
        private void Start()
        {
            _mAnimators = FindObjectsOfType<Animator>();
        }

        public void PlayCharacterAnimation(int animId)
        {
            foreach (var animator in _mAnimators)
            {
                animator.SetTrigger(_mAnimations[animId]);
            }
        }
    }
}
