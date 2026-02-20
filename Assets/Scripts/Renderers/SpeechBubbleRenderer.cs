using Interactions.BlobInteractions;
using UnityEngine;

namespace Renderers
{
    public class SpeechBubbleRenderer : MonoBehaviour
    {
        public SpriteRenderer speechBubbleContent;
        public GameObject speechBubbleModel;

        [Header("Request Sprites")]
        public Sprite greetingSprite;
        public Sprite complimentSprite;
        public Sprite insultSprite;
        public Sprite screamSprite;
        public Sprite giftSprite;
        
        [Header("Response Sprites")]
        public Sprite waveSprite;
        public Sprite complimentBackSprite;
        public Sprite insultBackSprite;
        public Sprite screamBackSprite;
        public Sprite thankYouSprite;

        private float _timeStamp;
        private float _maxSpeechBubbleTime = 2f;

        void Awake()
        {
            _timeStamp = 0f;
            speechBubbleModel = GameObject.Find("SpeechBubbleModel");
            HideContent();
        }

        void Update()
        {
            if (_timeStamp >= _maxSpeechBubbleTime)
            {
                HideContent();
                _timeStamp = 0f;
            }
            
            if (SpeechBubbleIsVisible()) _timeStamp += Time.deltaTime;
            //TODO Zeit zählen
            //TODO Zurücksetzen wenn überschrieben
        }

        public void Display(BlobInteractionType interactionType)
        {
            switch (interactionType)
            {
                case BlobInteractionType.Greeting:
                    DisplayContent(greetingSprite);
                    break;
                case BlobInteractionType.Insult:
                    DisplayContent(insultSprite);
                    break;
                case BlobInteractionType.Compliment:
                    DisplayContent(complimentSprite);
                    break;
                case BlobInteractionType.Scream:
                    DisplayContent(screamSprite);  
                    break;
                case BlobInteractionType.Gift:
                    DisplayContent(giftSprite);
                    break;
            }
        }
        
        public void Display(BlobInteractionResponseType responseType)
        {
            switch (responseType)
            {
                case BlobInteractionResponseType.ComplimentBack:
                    DisplayContent(complimentBackSprite);   
                    break;
                case BlobInteractionResponseType.InsultBack:
                    DisplayContent(insultBackSprite);   
                    break;
                case BlobInteractionResponseType.ScreamBack:
                    DisplayContent(screamBackSprite);
                    break;
                case BlobInteractionResponseType.Wave:
                    DisplayContent(waveSprite); 
                    break;
                case BlobInteractionResponseType.ThankYou:
                    DisplayContent(thankYouSprite); 
                    break;
            }
        }

        private void DisplayContent(Sprite sprite)
        {
            //TODO Speechbubble muss auftauchen
            //TODO TimeStamps -> Speech Bubble verschwindet
            
            _timeStamp = 0f;
            speechBubbleContent.sprite = sprite;
            speechBubbleModel.gameObject.SetActive(true);
        }

        private void HideContent()
        {
            //TODO verstecken
            speechBubbleModel.gameObject.SetActive(false);
        }
        
        private bool SpeechBubbleIsVisible()
        {
            return speechBubbleModel.gameObject.activeSelf;
        }
        
    }
}