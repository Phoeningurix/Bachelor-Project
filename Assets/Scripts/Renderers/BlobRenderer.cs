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
        
        [SerializeField] private Gradient emotionGradient;

        private BlobBrain _brain;
    
        private SpriteRenderer _spriteRenderer;
        
        private SpriteRenderer _bodyRenderer;
        
        private Vector3 _lastFramePosition;
    
        // Awake is called before Start
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _bodyRenderer = transform.Find("BlobBody").GetComponent<SpriteRenderer>();
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
            Debug.Log(_brain.name + " can now experience emotions");
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
            if (_brain.emotions["happiness"].Value > 0)
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
            
            UpdateColorGradient();

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
            _bodyRenderer.material = highlightMaterial;
        }

        private void OnUnselected()
        {
            _bodyRenderer.material = defaultMaterial;
        }
        
        public void UpdateColorSimple()
        {
            _bodyRenderer.color = Color.Lerp(Color.deepSkyBlue, Color.greenYellow, 
                _brain.emotions.GetBetween01("happiness"));
        }
        
        public void UpdateColorGradient()
        {
            float t = _brain.emotions.GetBetween01("happiness");
            _bodyRenderer.color = emotionGradient.Evaluate(t);
        }
        
    }
    
}
