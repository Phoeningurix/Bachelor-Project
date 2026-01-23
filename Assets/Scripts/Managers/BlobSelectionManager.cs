using System;
using AgentLogic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class BlobSelectionManager : MonoBehaviour
    {
        private PlayerInputActions _input;
        private BlobBrain _currentBlob;
        public event Action<BlobBrain> OnSelectionChanged;
        private Camera _camera;

        void Awake()
        {
            _input = new PlayerInputActions();
            _camera = Camera.main;
            
        }

        void OnEnable()
        {
            _input.Enable();
            _input.Player.Click.performed += OnClick;
            _input.Player.Cancel.performed += OnCancel;
        }

        void OnDisable()
        {
            _input.Disable();
            _input.Player.Click.performed -= OnClick;
            _input.Player.Cancel.performed -= OnCancel;
        }
        
        private void OnClick(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            Vector2 screenPos = _input.Player.Point.ReadValue<Vector2>();
            
            Vector2 worldPos = _camera.ScreenToWorldPoint(screenPos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            
            if (hit.collider != null)
            {
                BlobBrain brain = hit.collider.GetComponent<BlobBrain>();
                if (brain != null)
                {
                    SelectBlob(brain);
                    return;
                }
            }
            // Wenn UnselectBlob() bei jedem Klick ausgeführt wird, können die Slider nicht mehr benutzt werden.
            // UnselectBlob();
        }
        
        private void OnCancel(InputAction.CallbackContext context)
        {
            UnselectBlob();
        }

        private void SelectBlob(BlobBrain blob)
        {
            _currentBlob = blob;
            OnSelectionChanged?.Invoke(blob); 
            //Debug.Log($"Selected blob: {blob.name}");
        }

        private void UnselectBlob()
        {
            _currentBlob = null;
            OnSelectionChanged?.Invoke(null);
            //Debug.Log("Unselected blob");
            
        }

    }
}