using UnityEngine;

namespace Controllers.Physics.Ragdoll
{
    public class RagdollPhysicsController : MonoBehaviour
    {
        public float forceAmount = 500;

        private Rigidbody _selectedRigidbody;
        private Camera _targetCamera;
        private Vector3 _originalScreenTargetPosition;
        private Vector3 _originalRigidbodyPos;
        private float _selectionDistance;
        
        private void Start()
        {
            _targetCamera = Camera.main;
        }

        private void Update()
        {
            if (!_targetCamera)
                return;
        
            if (UnityEngine.Input.GetMouseButtonDown(0))
                // Check if we are hovering over Rigidbody, if so, select it
                _selectedRigidbody = GetRigidbodyFromMouseClick();
            
            if (UnityEngine.Input.GetMouseButtonUp(0) && _selectedRigidbody)
                // Release selected Rigidbody if there any
                _selectedRigidbody = null;
        }

        private void FixedUpdate()
        {
            if (!_selectedRigidbody) 
                return;
            
            var mousePositionOffset = _targetCamera.ScreenToWorldPoint(new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, _selectionDistance)) - _originalScreenTargetPosition;
            _selectedRigidbody.velocity = (_originalRigidbodyPos + mousePositionOffset - _selectedRigidbody.transform.position) * (forceAmount * Time.deltaTime);
        }

        private Rigidbody GetRigidbodyFromMouseClick()
        {
            var ray = _targetCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            var hit = UnityEngine.Physics.Raycast(ray, out var hitInfo);
            if (!hit) 
                return null;

            var rigidBody = hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            if (!rigidBody)
                return null;
            
            _selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
            _originalScreenTargetPosition = _targetCamera.ScreenToWorldPoint(new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, _selectionDistance));
            _originalRigidbodyPos = hitInfo.collider.transform.position;
            
            return rigidBody;
        }
    }
}
