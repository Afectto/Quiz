using System;
using Grid;
using UnityEngine;
using UnityEngine.UI;

namespace CanvasOverlay
{
    public class CanvasOverlayEndRound : CanvasOverlayBase
    {
        [SerializeField] private Text QuestText;

        protected override void Start()
        {
            base.Start();

            CreateGrid.onCompleteGame += OnCompleteGame;
            DOTweenAnimator.OnFadeAnimationCanvasGroupComplete += Deactivate;
        }
        
        protected override void Deactivate()
        {
            base.Deactivate();
            QuestText.gameObject.SetActive(true);
        }

        private void OnCompleteGame()
        {
            QuestText.gameObject.SetActive(false);
            gameObject.SetActive(true);
            OnStartFadeAnimationCanvas?.Invoke(_canvasGroup, 0.75f, true);
        }

        private void OnDestroy()
        {
            CreateGrid.onCompleteGame -= OnCompleteGame;
        }
    }
}
