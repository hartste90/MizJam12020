using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
public class OutroController : MonoBehaviour
{
    public CanvasGroup backgroundPanel;

    public Transform congratsText;

    public Transform replayButton;

    public PostProcessVolume volume;
    
    public void PlayIntro()
    {
        congratsText.DOLocalMoveY(-25f, 1f).SetEase(Ease.OutQuad);
        replayButton.DOLocalMoveY(-117f, 1f).SetEase(Ease.OutQuad);
    }

    public void PlayOutro(UnityAction animationComplete)
    {
        congratsText.DOLocalMoveY(500f, 1f).SetEase(Ease.InSine);
        replayButton.DOLocalMoveY(-500f, 1f).SetEase(Ease.InSine).OnComplete(() => { animationComplete?.Invoke();});
    }


}
