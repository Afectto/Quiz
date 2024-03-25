using System;
using Grid;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOverlay : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public static Action<CanvasGroup, bool> OnCompleteGameCanvas;
    [SerializeField] private Text QuestText;
        
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
        QuestText.gameObject.SetActive(false);
        gameObject.SetActive(true);
        OnCompleteGameCanvas?.Invoke(_canvasGroup, true);
    }

    private void Deactivate()
    {
        QuestText.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        CreateGrid.onCompleteGame -= OnCompleteGame;
    }
}
