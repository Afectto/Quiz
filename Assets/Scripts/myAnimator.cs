using System;
using DG.Tweening;
using Grid;
using UnityEngine;

public class myAnimator : MonoBehaviour
{
    [SerializeField] private ParticleSystem ParticleSystem;

    public static Action OnNeedChangeLevel;
    public static Action OnFadeAnimationCanvasGroupComplete;
    
    private void Awake()
    {
        Slot.OnClickSlotAnimation += OnClickSlotAnimation;
        CreateGrid.onStartFirstLevel += onStartFirstLevel;
        CanvasOverlay.OnCompleteGameCanvas += FadeAnimationCanvasGroup;
        RestartGame.OnStartFadeAnimationNewGame += FadeAnimationCanvasGroup;
    }

    private void onStartFirstLevel(Transform transformObject)
    {
        BounceAnimation(transformObject);
    }

    private void OnClickSlotAnimation(Transform transformObject, bool isSuccess)
    {
        if (!isSuccess)
        {
            TwitchAnimation(transformObject);
        }
        else
        {
            BounceAnimation(transformObject, isSuccess);
            ParticleSystem.transform.position = transformObject.position;
            ParticleSystem.Play();
        }
    }

    private void TwitchAnimation(Transform transformObject)
    {
        Sequence mySequence = DOTween.Sequence();
        if (transformObject.rotation == Quaternion.identity)
        {
            mySequence.Append(transformObject.DOLocalMoveX(-0.1f, 0.2f));
            mySequence.Append(transformObject.DOLocalMoveX(0, 0.2f));
            mySequence.Append(transformObject.DOLocalMoveX(0.1f, 0.2f));
            mySequence.Append(transformObject.DOLocalMoveX(0, 0.2f));
        }
        else
        {
            mySequence.Append(transformObject.DOLocalMoveY(-0.1f, 0.2f));
            mySequence.Append(transformObject.DOLocalMoveX(0, 0.2f));
            mySequence.Append(transformObject.DOLocalMoveY(0.1f, 0.2f));
            mySequence.Append(transformObject.DOLocalMoveX(0, 0.2f));
        }
        
        mySequence.SetEase(Ease.InBounce);
        mySequence.Play();
    }

    private void BounceAnimation(Transform transformObject, bool isSuccess = false)
    {
        var scale = transformObject.localScale;
        
        var mySequence = DOTween.Sequence();
        mySequence.Append(transformObject.DOScale(0f, 0f));
        mySequence.Append(transformObject.DOScale(scale * 1.1f, 0.75f));
        mySequence.Append(transformObject.DOScale(scale, 0.5f));
        
        if (isSuccess)
        {
            mySequence.OnComplete(() =>
            {
                OnNeedChangeLevel?.Invoke();
            });
        }
        
        mySequence.Play();
    }

    private void FadeAnimationCanvasGroup(CanvasGroup canvasGroup, bool isIn = true)
    {
        var mySequence = DOTween.Sequence();
        mySequence.Append(canvasGroup.DOFade(isIn ? 1f : 0, 0.75f));
        if (!isIn)
        {
            mySequence.OnComplete(() =>
            {
                OnFadeAnimationCanvasGroupComplete?.Invoke();
            });
        }
        mySequence.Play();
    }

    private void OnDestroy()
    {
        Slot.OnClickSlotAnimation -= OnClickSlotAnimation;
        CreateGrid.onStartFirstLevel -= onStartFirstLevel;
        CanvasOverlay.OnCompleteGameCanvas -= FadeAnimationCanvasGroup;
        RestartGame.OnStartFadeAnimationNewGame -= FadeAnimationCanvasGroup;
    }
}

