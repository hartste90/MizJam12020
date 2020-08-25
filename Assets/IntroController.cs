using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class IntroController : MonoBehaviour
{
    public Transform textPanel;
    public Transform playButton;
    
    void Awake()
    {
        textPanel.localPosition = Vector3.up * 500f;
        playButton.localPosition = Vector3.up * -500f;
    }
    
    public void PlayIntro()
    {
        textPanel.DOLocalMoveY(11f, 1f).SetEase(Ease.OutQuad);
        playButton.DOLocalMoveY(-69f, 1f).SetEase(Ease.OutQuad);
    }

    public void PlayOutro(UnityAction animationComplete)
    {
        textPanel.DOLocalMoveY(500f, 1f).SetEase(Ease.InSine);
        playButton.DOLocalMoveY(-500f, 1f).SetEase(Ease.InSine).OnComplete(() => { animationComplete?.Invoke();});
    }

}
