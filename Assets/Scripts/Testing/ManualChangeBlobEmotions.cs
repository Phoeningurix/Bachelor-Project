using System;
using AgentLogic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Testing
{
    [RequireComponent(typeof(BlobBrain))]
    public class ManualChangeBlobEmotions : MonoBehaviour
    {
        private BlobBrain _brain;
        public string emotionToChange = "happiness";
        public float changeAmount = 0.1f;

        void Awake()
        {
            _brain = GetComponent<BlobBrain>();
        }

        void Update()
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (_brain.emotions[emotionToChange].Value + changeAmount > 1)
                {
                    _brain.emotions[emotionToChange].Value = 1;
                }
                else
                {
                    _brain.emotions[emotionToChange].Value += changeAmount;
                }
            }
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                if (_brain.emotions[emotionToChange].Value - changeAmount < -1)
                {
                    _brain.emotions[emotionToChange].Value = -1;
                }
                else
                {
                    _brain.emotions[emotionToChange].Value -= changeAmount;
                }
            }
        }

        private void OnValidate()
        {
            if (changeAmount < 0f) changeAmount = 0f;
        }
    }
}