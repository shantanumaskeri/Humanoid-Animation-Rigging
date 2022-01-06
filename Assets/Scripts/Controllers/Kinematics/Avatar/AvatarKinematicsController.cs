using UnityEngine;

namespace Controllers.Kinematics.Avatar
{
	public class AvatarKinematicsController : MonoBehaviour 
	{
		public Animator animator;

		public enum IkTargets
		{
			BlueCube,
			GreenCube,
			RedSphere,
			YellowSphere
		}

		public enum LookAtTargets
		{
			BlueCube,
			GreenCube,
			RedSphere,
			YellowSphere
		}
		
		public IkTargets ikTargets = IkTargets.BlueCube;
		public LookAtTargets lookAtTargets = LookAtTargets.RedSphere;

		[Range(0f, 1f)]
		public float weightForPosition;
		[Range(0f, 1f)]
		public float weightForRotation;
		[Range(0f, 1f)]
		public float lookAtWeight = 1.0f;
		[Range(0f, 1f)]
		public float headWeight = 0.8f;
		[Range(0f, 1f)]
		public float bodyWeight = 0.6f;
		[Range(0f, 1f)]
		public float eyeWeight = 1.0f;
		[Range(0f, 1f)]
		public float clampWeight = 0.2f;

		public AvatarIKGoal avatarIkBodyPart1;
		public AvatarIKGoal avatarIkBodyPart2;

		public AvatarIKHint avatarIkHintBodyPart1;
		public AvatarIKHint avatarIkHintBodyPart2;

		public HumanBodyBones humanBodyBones;
		
		private GameObject _ikTarget;
		private GameObject _lookAtTarget;
		
		private float _vs;
		private float _hs;

		private void OnAnimatorIK(int layerIndex)
		{
			_ikTarget = GameObject.Find(ikTargets.ToString());
			_lookAtTarget = GameObject.Find(lookAtTargets.ToString());
			
			var lookAtTargetPosition = _lookAtTarget.transform.position;
			var lookAtTargetRotation = _lookAtTarget.transform.rotation;
			var ikTargetPosition = _ikTarget.transform.position;
			var ikTargetRotation = _ikTarget.transform.rotation;
			
			animator.SetIKPositionWeight(avatarIkBodyPart1, weightForPosition);
			animator.SetIKPosition(avatarIkBodyPart1, ikTargetPosition);
			
			animator.SetIKRotationWeight(avatarIkBodyPart1, weightForRotation);
			animator.SetIKRotation(avatarIkBodyPart1, ikTargetRotation);

			animator.SetIKPositionWeight(avatarIkBodyPart2, weightForPosition);
			animator.SetIKPosition(avatarIkBodyPart2, lookAtTargetPosition);
			
			animator.SetIKRotationWeight(avatarIkBodyPart2, weightForRotation);
			animator.SetIKRotation(avatarIkBodyPart2, lookAtTargetRotation);
			
			animator.SetIKHintPosition(avatarIkHintBodyPart1, lookAtTargetPosition);
			animator.SetIKHintPositionWeight(avatarIkHintBodyPart1, weightForPosition);
			
			animator.SetIKHintPosition(avatarIkHintBodyPart2, lookAtTargetPosition);
			animator.SetIKHintPositionWeight(avatarIkHintBodyPart2, weightForPosition);
			
			animator.SetBoneLocalRotation(humanBodyBones, lookAtTargetRotation);
			
			animator.SetLookAtWeight(lookAtWeight, headWeight, bodyWeight, eyeWeight, clampWeight);
			animator.SetLookAtPosition(lookAtTargetPosition);
		}
		
		private void OnAnimatorMove() 
		{
			_vs = UnityEngine.Input.GetAxis("Vertical");
			_hs = UnityEngine.Input.GetAxis("Horizontal");

			transform.Translate(new Vector3(0, 0, _vs * Time.deltaTime * 2.0f));
			transform.Rotate(new Vector3(0, _hs * Time.deltaTime * 50.0f, 0));
		}
	}
}
