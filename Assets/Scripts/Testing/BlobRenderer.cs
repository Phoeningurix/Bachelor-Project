using UnityEngine;

namespace Testing
{
    public class BlobRenderer : MonoBehaviour
    {
        
        public Sprite spriteHappyBlob;
        public Sprite spriteNeutralBlob;
        public Sprite spriteSadBlob;

        private BlobBrain _brain;
    
        private SpriteRenderer _spriteRenderer;
        
        private Vector3 _lastFramePosition;
    
        // Awake is called before Start
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _brain = GetComponentInParent<BlobBrain>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Blob can now experience emotions");
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
    }
}
