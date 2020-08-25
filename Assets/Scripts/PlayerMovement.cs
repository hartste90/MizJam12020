using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Activatable
{
    public CharacterController2D controller2D;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    private float lastMoveTime;
    private float moveEffectRate = .25f;

    private bool isEnabled = false;

    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        if(isEnabled)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (horizontalMove != 0)
            {
                LevelController.Instance.playerController.PlayRunAnimation();
                // if (CharacterController2D.IsGrounded() && Time.time - lastMoveTime >= moveEffectRate)
                // {
                //     lastMoveTime = Time.time;
                //     PlayerController.PlayGroundedRunning();
                // }

            }
            else
            {
                LevelController.Instance.playerController.PlayIdleAnimation();
            }

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                // transform.localRotation = Quaternion.Euler(Vector3.zero);
                // PlayerController.PlayJumpAnimation();

            }
        }
    }

    void LateUpdate()
    {
        if (isEnabled)
        {
            if (LevelController.Instance.playerController.IsAlive())
            {
                controller2D.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
                jump = false;
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        // Debug.Log("Collided: " + col.gameObject.layer.ToString());
        if (col.gameObject.layer == LayerMask.NameToLayer("MovingPlatform"))
             transform.parent = col.transform;

    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("MovingPlatform"))
             transform.parent = null;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log("Collided: " + col.gameObject.layer.ToString());
        if (col.gameObject.layer == LayerMask.NameToLayer("MovingPlatform"))
             transform.parent = col.transform;

    }
}
