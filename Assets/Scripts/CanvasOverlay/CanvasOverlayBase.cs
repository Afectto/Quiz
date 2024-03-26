using System;
using UnityEngine;

namespace CanvasOverlay
{
    public abstract class CanvasOverlayBase : MonoBehaviour
    {
        protected CanvasGroup _canvasGroup;
        public static Action<CanvasGroup, float, bool> OnStartFadeAnimationCanvas;

        protected virtual void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            Deactivate();
        }
        
        protected virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}