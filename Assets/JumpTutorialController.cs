using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class JumpTutorialController : MonoBehaviour
{
    // public Transform keyUp;
    // public Transform keyDown;
    public Transform keyToCTA;
    public Transform doubleWord;
    public Transform jumpWord;

    private Vector3 ogScale;


    public float punchAmt = -.2f;
    // Start is called before the first frame update
    void Start()
    {
        ogScale = doubleWord.localScale;
        PlayTutorialAnimation();
    }

    void PlayTutorialAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(keyToCTA.DOPunchScale(Vector3.one * punchAmt, .1f, 0, 0));
        seq.AppendInterval(.15f);
        seq.SetLoops(-1).SetEase(Ease.Linear);
        seq.Play();

        Sequence seq2 = DOTween.Sequence();
        seq2.Append(doubleWord.DOPunchScale(ogScale * punchAmt, .1f, 0, 0));
        seq2.AppendInterval(.25f);
        seq2.Append(jumpWord.DOPunchScale(ogScale * punchAmt, .1f, 0, 0));
        seq2.SetLoops(-1).SetEase(Ease.Linear).SetDelay(.35f);
        seq2.Play();
        
    }
}
