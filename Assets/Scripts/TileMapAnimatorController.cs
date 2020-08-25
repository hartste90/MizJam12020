using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class TileMapAnimatorController : MonoBehaviour
{
    public Transform inPosition;
    public Transform outPosition;
    private Animator anim;
    
    private UnityAction onIntroCompleteCallback;
    private UnityAction onOutroCompleteCallback;
    void Awake()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    void Start()
    {
        transform.position = outPosition.position;
    }
    public void PlayIntro(UnityAction onComplete)
    {
        gameObject.SetActive(true);
        // onIntroCompleteCallback = onComplete;
        // anim.SetTrigger("AnimateIn");
        transform.DOMove(Vector3.zero, .5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => { onComplete.Invoke(); OnIntroComplete(); });
    }

    public void PlayOutro(UnityAction onComplete)
    {
        // onOutroCompleteCallback = onComplete;
        // anim.SetTrigger("AnimateOut");
        
        Activatable[] activatables = GetComponentsInChildren<Activatable>();
        foreach(Activatable activatable in activatables)
        {
            activatable.Deactivate();
        }

        transform.DOMove(outPosition.position, .5f)
            .OnComplete(() => { onComplete?.Invoke(); });
    }

    public void OnIntroComplete()
    {
        Debug.Log("OnIntroCOmplete");
        Activatable[] activatables = GetComponentsInChildren<Activatable>();
        foreach(Activatable activatable in activatables)
        {
            activatable.Activate();
        }
        onIntroCompleteCallback?.Invoke();
        

    }
    public void OnOutroComplete()
    {
        onOutroCompleteCallback?.Invoke();
    }
}
