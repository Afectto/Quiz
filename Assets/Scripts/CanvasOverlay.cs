using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOverlay : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public static Action<CanvasGroup, bool> OnCompleteGameCanvas;
    
    void Start()
    {
        CreateGrid.onCompleteGame += OnCompleteGame;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        Deactivate();

        myAnimator.OnFadeAnimationCanvasGroupComplete += Deactivate;

    }

    private void OnCompleteGame()
    {
        gameObject.SetActive(true);
        OnCompleteGameCanvas?.Invoke(_canvasGroup, true);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        CreateGrid.onCompleteGame -= OnCompleteGame;
    }
}
