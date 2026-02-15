using System.Collections.Generic;
using Interactions;
using UnityEngine;

namespace Renderers
{
    public class AppleRenderer : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            
        }

        public void SetVisible(bool visible)
        {
            _renderer.enabled = visible;
        }

        public bool IsVisible()
        {
            return _renderer.enabled;
        }
        
    }
}