using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Gradient toLifeGradient;
    public GameObject colliderGroup;
    public List<AudioClip> audioClips;
    //clips = 0:death, 1:jump/land
    public ParticleSystem puffSystem;
    public ParticleSystem landingSystem;
    public ParticleSystem deathSystem;
    public PlayerMovement playerMovement;
    public TextMesh idText;
    private SpriteRenderer sprite;
    
    private Animator animator;
    private AudioSource audio;
    public bool isAlive = true;
    public void Awake()
    {
        audio = GetComponentInChildren<AudioSource>();
        puffSystem = GetComponentInChildren<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isAlive = true;

        CharacterController2D.Instance.OnLandEvent.AddListener(PlayLandingParticles);
        CharacterController2D.Instance.OnLandEvent.AddListener(StopJumpAnimation);
    }
    void Start()
    {
        // CharacterController2D.Instance.OnLandEvent.AddListener(PlayLandEffect);
        HandlePlayerLife();

    }
    public void PlayGroundedRunning()
    {
        MakePuff();

    }

    public void PlayJumpAnimation()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", true);
    }


    public void PlayIdleAnimation()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("Running", false);
    }

    public void PlayRunAnimation()
    {
        animator.SetBool("Running", true);
    }

    public void StopJumpAnimation()
    {
        animator.SetBool("Jumping", false);
    }

    public void HandlePlayerDeath()
    {
        isAlive = false;
        CharacterController2D.Disable();
        playerMovement.Disable();
        GetComponent<Rigidbody2D>().isKinematic = false;
        PlayDeathSequence();
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    void PlayDeathSequence()
    {
        audio.PlayOneShot(audioClips[0]);
        sprite.flipY = true;
        colliderGroup.SetActive(false);
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10f + Vector3.right * Random.Range(-10f, 10f), ForceMode2D.Impulse);
        sprite.DOFade(0f, 1.5f).OnComplete(OnDeathComplete);
        deathSystem.Play();

    }

    void OnDeathComplete()
    {
        LevelController.HandlePlayerDeath();
    }

    public void HandlePlayerLife()
    {
        PlayNewLifeAnimation();
    }

    public void PlayNewLifeAnimation()
    {
        idText.gameObject.SetActive(false);
        idText.text = GetRandomIDName();
        // idText.color = Color.clear;
        // sprite.DOGradientColor(toLifeGradient, 2f).OnComplete(() => { CharacterController2D.Enable(); })
        Sequence seq = DOTween.Sequence();
        seq.Append(sprite.DOFade(0, 0f));
        seq.AppendInterval(.4f);
        seq.AppendCallback(() => { idText.gameObject.SetActive(true);});
        seq.Append(sprite.DOFade(1f, 0f));
        seq.AppendInterval(.4f);
        
        seq.Append(sprite.DOFade(0, 0f));
        seq.AppendInterval(.2f);
        seq.Append(sprite.DOFade(1f, 0f));
        seq.AppendInterval(.2f);

        seq.Append(sprite.DOFade(0, 0f));
        seq.AppendInterval(.1f);
        seq.Append(sprite.DOFade(1f, 0f));
        seq.AppendInterval(.1f);

        seq.Append(sprite.DOFade(0, 0f));
        seq.AppendInterval(.1f);
        seq.Append(sprite.DOFade(1f, 0f));
        seq.AppendInterval(.1f);
        seq.Play().OnComplete(() => 
            { 
                playerMovement.Enable();
                CharacterController2D.Enable();
                idText.gameObject.SetActive(false);
            });
        
    }

    private string GetRandomIDName()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var stringChars = new char[4];
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new string(stringChars);
        finalString = finalString.Insert(2, "-");
        finalString = finalString.Insert(0, "ID: ");
        return finalString;
    }

    public void MakePuff()
    {
        puffSystem.Play();
    }

    public void PlayLandingParticles()
    {   
        Debug.Log("Alive? " + isAlive);
        if (isAlive)
        {
            landingSystem.Play();
            audio.PlayOneShot(audioClips[1]);
        }
    }
}
