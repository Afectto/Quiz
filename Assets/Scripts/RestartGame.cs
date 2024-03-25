using System;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    
    public static Action<CanvasGroup, bool> OnStartFadeAnimationNewGame;
    public static Action OnStartNewGame;
    
    public void StartNewGame()
    {
        OnStartFadeAnimationNewGame?.Invoke(_canvasGroup, false);
        
        OnStartNewGame?.Invoke();
    }
    
    
}
