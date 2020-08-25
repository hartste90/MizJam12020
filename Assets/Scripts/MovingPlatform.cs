using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovingPlatform : Activatable
{
    public Transform startPos;
    public Transform endPos;
    public Transform targetToMove;
    public float moveTime = 3f;
    // Start is called before the first frame update

    void Awake()
    {
        targetToMove.position = startPos.position;
    }
    public override void Activate()
    {
        BeginLoopingAnimation();
    }

    public override void Deactivate()
    {
        DOTween.Kill("Activatable: " + gameObject.GetInstanceID());
    }
    void BeginLoopingAnimation()
    {
        Debug.Log("Begining loop anim");
        Sequence seq = DOTween.Sequence();
        seq.Append(targetToMove.DOLocalMove(endPos.localPosition, moveTime).SetEase(Ease.InOutQuad));
        seq.SetLoops(-1, LoopType.Yoyo);
        seq.Play().SetId("Activatable: " + gameObject.GetInstanceID());
    }
}

public class Activatable: MonoBehaviour
{
    public virtual void Activate(){}
    public virtual void Deactivate(){}
}