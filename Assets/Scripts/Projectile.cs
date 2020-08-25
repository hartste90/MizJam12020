using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Projectile: MonoBehaviour
{
    public bool usesPhysics = true;

    private bool isLiveAmmo = false;
    private float timeToLive = .05f;
    private float instantiatedTime;

    void Awake()
    {
        instantiatedTime = Time.time;
        GetComponentInChildren<CircleCollider2D>().enabled = false;
        GetComponentInChildren<Rigidbody2D>().isKinematic = !usesPhysics;
    }

    void Update()
    {
        if (!isLiveAmmo && Time.time - instantiatedTime > timeToLive)
        {
            isLiveAmmo = true;
            GetComponentInChildren<CircleCollider2D>().enabled = true;
        }
    }



    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.layer != LayerMask.NameToLayer("Player"))
    //     {
    //         HandleHitEnvironment(col);
    //     }
    // }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            HandleHitEnvironment(col);
        }
    }

    private void HandleHitEnvironment(Collision2D collision)
    {
        foreach (CircleCollider2D col in GetComponentsInChildren<CircleCollider2D>())
        {
            col.enabled = false;
        }
        // GetComponentInChildren<Rigidbody2D>().simulated = false;
        GetComponent<Rigidbody2D>().AddForce(-collision.contacts[0].normal + new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)));
        GetComponentInChildren<SpriteRenderer>().DOFade(0f, .5f).OnComplete(KillSelf);
        GetComponentInChildren<AudioSource>().Play();
    }

    void KillSelf()
    {
        Destroy(gameObject);
    }

}
