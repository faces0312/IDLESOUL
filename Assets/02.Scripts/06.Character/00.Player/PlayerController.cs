using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player player;
    [Header("Movement")]
    public float m_fSpeed = 5.0f;
    public float inputThreashold;
    public Vector2 curMovementInput;

    public event Action<Vector2> OnDirectionChanged; // 진행 방향 이벤트
    public event Action OnSkill1; // J키 스킬 이벤트
    public event Action OnSkill2; // K키 스킬 이벤트
    public event Action<int> OnSwitch; // 스킬 틀 전환 이벤트


    private void Start()
    {
        GameManager.Instance.playerController = this;
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (curMovementInput.magnitude > 0)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 dir = new Vector3(curMovementInput.x, 0.0f, curMovementInput.y);
        dir *= m_fSpeed;
        dir.y = player.rb.velocity.y;

        player.rb.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();

            if (inputVector.magnitude > inputThreashold)
            {
                if (player.isJoyStick == false)
                {
                    player.playerStateMachine.ChangeState(player.playerStateMachine.MoveState);
                    if (inputVector.x < 0)
                        player.playerStateMachine.CurrentState.FlipCharacter(false);
                    else if (inputVector.x > 0)
                        player.playerStateMachine.CurrentState.FlipCharacter(true);
                }
                curMovementInput = inputVector;
                GameManager.Instance.joyStick.AutoFalse();
                player.isController = true;
                OnDirectionChanged?.Invoke(curMovementInput);
            }
            else
            {
                curMovementInput = Vector2.zero;
                player.isController = false;
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            player.isController = false;
            if(player.isJoyStick == false)
            {
                player.targetSearch.TargetClear();
                player.playerStateMachine.ChangeState(player.playerStateMachine.IdleState);
            }
        }
    }

    public void OnSkill1Action(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            UseSkill1();
        }
    }

    public void OnSkill2Action(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            UseSkill2();
        }
    }

    public void UseSkill1()
    {
        OnSkill1?.Invoke();
    }

    public void UseSkill2()
    {
        OnSkill2?.Invoke();
    }

    public void OnSwitchAction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            int switchIndex = 0;

            // 입력된 키에 따라 switchIndex 설정
            if (context.control.name == "1")
                switchIndex = 1;
            else if (context.control.name == "2")
                switchIndex = 2;
            else if (context.control.name == "3")
                switchIndex = 3;

            if (switchIndex != 0)
            {
                SwitchSKill(switchIndex);
                GameManager.Instance.joyStick.FindSkillButtons();
            }
        }
    }

    public void SwitchSKill(int _index)
    {
        OnSwitch?.Invoke(_index);
    }
}
