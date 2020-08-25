using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoalController : MonoBehaviour
{
    public SpriteRenderer gongOutside;
    public SpriteRenderer gongInside;
    bool isTriggered;
    AudioSource audio;

    void Awake()
    {
        audio = GetComponentInChildren<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isTriggered && col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HandleGoalReached();
        }
    }

    void HandleGoalReached()
    {
        isTriggered = true;
        //signal successful end of level
        PlayGoalAnimation();
        PlayGoalSound();
        LevelController.HandleGoalReached();
    }
    

    void PlayGoalSound()
    {
        audio.pitch = Random.Range(.5f, 1f);
        audio.Play();
    }
    void PlayGoalAnimation()
    {
        gongInside.transform.DOShakeScale(1f);
        gongInside.transform.DOLocalMoveY(1f, 1f);
        gongInside.transform.DOLocalRotate(Vector3.up * 1440, 2f, RotateMode.FastBeyond360);
        gongInside.DOFade(0f, 1f);
        gongOutside.DOFade(0f, .9f);
        // GetComponentInChildren<SpriteRenderer>().DOFade(0f, 1f);//.OnComplete(
            // () => { Destroy(gameObject); });        
    }
}
