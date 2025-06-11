using System;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public static event Action<bool> OnBlock;
    [SerializeField] private GameObject leftSword;
    [SerializeField] private GameObject rightSword;

    [SerializeField] private float timer = 1f;
    private float time;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
     
        CharacterMovement.OnMoveAnimation += HandlerMoveAnimation;
        CharacterMovement.OnJump += HandlerJumpAnimation;
        CharacterMovement.OnGround += HandlerLandAnimation;
    
        CharacterCombat.OnAttack += HandlerAttackAnimation;

        time = 0f;
    }

    private void Update()
    {
        if (PlayerRespawnManager.instance.IsPlayerDead())
        {
            return;
        }

        if (time > 0f)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0f && Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("isRoll");
            time = timer;
        }

        if (Input.GetMouseButton(2))
        {
            anim.SetBool("isBlock", true);
            OnBlock?.Invoke(true);
        }

        if (Input.GetMouseButtonUp(2))
        {
            anim.SetBool("isBlock", false);
            OnBlock?.Invoke(false);
        }
    }

    private void HandlerMoveAnimation(float x, float z)
    {
        if(x != 0 || z != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        anim.SetFloat("x", x);
        anim.SetFloat("z", z);

        if(Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("z", 1.2f);
            }
        
            if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("x", -1.2f);
            }

            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("x", 1.2f);
            }
        }
    }

    private void HandlerJumpAnimation(bool isJump)
    {
        anim.SetBool("isJumping", isJump);
    }

    private void HandlerLandAnimation(bool isGrounded)
    {
        if (isGrounded)
        {
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
        else
        {
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isFalling", true);
        }
    }

    private void HandlerAttackAnimation(int currentAttack)
    {
        switch (currentAttack)
        {
            case 0: anim.SetTrigger("leftAttack");
                break;
            case 1: anim.SetTrigger("rightAttack");
                break;
            default:
                break;
        }
    }

    #region AnimationTrigger

    #region Left

    private void SwitchOnLeftSword()
    {
        leftSword.SetActive(true);
    }

    private void SwitchOffLeftSword()
    {
        leftSword.SetActive(false);
    }

    #endregion

    #region Right

    private void SwitchOnRightSword()
    {
        rightSword.SetActive(true);
    }

    private void SwitchOffRightSword()
    {
        rightSword.SetActive(false);
    }

    #endregion

    #endregion


    private void OnDisable()
    {
        CharacterMovement.OnMoveAnimation -= HandlerMoveAnimation;
        CharacterMovement.OnJump -= HandlerJumpAnimation;
        CharacterMovement.OnGround -= HandlerLandAnimation;

        CharacterCombat.OnAttack -= HandlerAttackAnimation;
    }
}