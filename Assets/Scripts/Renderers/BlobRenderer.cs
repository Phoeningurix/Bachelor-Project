using AgentLogic;
using UnityEngine;

namespace Renderers
{
    public class BlobRenderer : MonoBehaviour
    {
        
        [Header("Sprites")]
        public Sprite spriteHappyBlob;
        public Sprite spriteNeutralBlob;
        public Sprite spriteSadBlob;
        
        [Header("Materials")]
        public Material defaultMaterial;
        public Material highlightMaterial;

        private BlobBrain _brain;
    
        private SpriteRenderer _spriteRenderer;
        
        private Vector3 _lastFramePosition;
    
        // Awake is called before Start
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _brain = GetComponentInParent<BlobBrain>();
        }

        private void OnEnable()
        {
            _brain.OnSelected += OnSelected;
            _brain.OnUnselected += OnUnselected;
        }

        private void OnDisable()
        {
            _brain.OnSelected -= OnSelected;
            _brain.OnUnselected -= OnUnselected;
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Blob can now experience emotions");
            _spriteRenderer.material = defaultMaterial;
        }

        //Set Sprite
        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        // Update is called once per frame
        void Update()
        {
            if (_brain.emotions["happiness"].Value> 0)
            {
                SetSprite(spriteHappyBlob);
            } else if (_brain.emotions["happiness"].Value < 0)
            {
                SetSprite(spriteSadBlob);
            }
            else
            {
                SetSprite(spriteNeutralBlob);
            }

            if (_lastFramePosition.x < transform.parent.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            } else if (_lastFramePosition.x > transform.parent.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            _lastFramePosition = transform.parent.position;
        }
        
        
        private void OnSelected()
        {
            _spriteRenderer.material = highlightMaterial;
        }

        private void OnUnselected()
        {
            _spriteRenderer.material = defaultMaterial;
        }
        
    }
    
}
