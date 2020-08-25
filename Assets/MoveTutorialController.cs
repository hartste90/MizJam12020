using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveTutorialController : MonoBehaviour
{
    // public Transform keyUp;
    // public Transform keyDown;
    public Transform keyLeft;
    public Transform keyRight;

    public float punchAmt = -.2f;
    // Start is called before the first frame update
    void Start()
    {
        PlayTutorialAnimation();
    }

    void PlayTutorialAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(keyLeft.DOPunchScale(Vector3.one * punchAmt, .1f, 0, 0));
        seq.AppendInterval(.5f);
        seq.Append(keyRight.DOPunchScale(Vector3.one * punchAmt, .1f, 0, 0));
        seq.AppendInterval(.5f);
        seq.SetLoops(-1).SetEase(Ease.Linear);
        seq.Play();
        
    }
}
