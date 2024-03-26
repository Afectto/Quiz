using System;
using Grid;

namespace CanvasOverlay
{
    public class CanvasOverlayLoading : CanvasOverlayBase
    {
        protected override void Start()
        {
            base.Start();
            RestartGame.OnStartNewGame += OnStartNewGame;
            CreateGrid.onStartFirstLevel += OnShowNewLevel;
            DOTweenAnimator.OnFadeAnimationCanvasGroupComplete += Deactivate;
        }

        private void OnShowNewLevel()
        {
            OnStartFadeAnimationCanvas?.Invoke(_canvasGroup, 1f, false);
        }

        private void OnStartNewGame()
        {
            gameObject.SetActive(true);
            OnStartFadeAnimationCanvas?.Invoke(_canvasGroup, 0.1f, true);
        }
        
        

        private void OnDestroy()
        {
            RestartGame.OnStartNewGame -= OnStartNewGame;
            CreateGrid.onStartFirstLevel -= OnShowNewLevel;
        }
    }
}
