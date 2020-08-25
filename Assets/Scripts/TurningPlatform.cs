using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TurningPlatform : Activatable
{
    public Transform targetToRotate;
    public float rotateTime = 3f;
    public float restTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        BeginLoopingAnimation();
    }

    void BeginLoopingAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(targetToRotate.DOShakePosition(.3f, .1f));
        seq.Append(targetToRotate.DOLocalRotate(Vector3.forward * 180f, rotateTime).SetEase(Ease.InOutQuad));
        seq.AppendInterval(restTime);
        seq.Append(targetToRotate.DOShakePosition(.3f, .1f));
        seq.Append(targetToRotate.DOLocalRotate(Vector3.forward * 360f, rotateTime).SetEase(Ease.InOutQuad));
        seq.AppendInterval(restTime);
        seq.SetLoops(-1, LoopType.Restart);
        seq.Play();
    }
}
