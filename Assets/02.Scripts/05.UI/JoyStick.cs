using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform m_rectBack; // 조이스틱 배경
    public RectTransform m_rectJoystick; // 조이스틱 노브
    public Player player; // 플레이어의 Rigidbody
    public float m_fRadius; // 조이스틱 배경의 반지름
    public float m_fSpeed = 5.0f; // 이동 속도
    private Vector3 m_vecMove; // 이동 벡터

    [Header("Auto")]
    public GameObject onImage;
    public GameObject offImage;
    int currentSoulIndex = 1;
    [SerializeField] private SkillButton skill1Button;
    [SerializeField] private SkillButton skill2Button;
    private Coroutine autoSkillCoroutine;

    void Awake()
    {
        GameManager.Instance.joyStick = this;
        player = GameManager.Instance.player.GetComponent<Player>();
        m_fRadius = m_rectBack.rect.width * 0.5f;
    }

    void Start()
    {
        FindSkillButtons();
        StartCoroutine(DelayedAutoButton());
    }
    private IEnumerator DelayedAutoButton()
    {
        yield return null;
        AutoButtton();
    }

    public void FindSkillButtons()
    {
        SkillButton[] skillButtons = FindObjectsOfType<SkillButton>();
        foreach (SkillButton skillButton in skillButtons)
        {
            if (skillButton.skillType == SkillType.Default)
            {
                skill1Button = skillButton;
            }
            else if (skillButton.skillType == SkillType.Ultimate)
            {
                skill2Button = skillButton;
            }
        }
    }

    void FixedUpdate()
    {
        if (player.isJoyStick && !GameManager.Instance.playerController.isStunned)
        {
            player.rb.velocity = new Vector3(m_vecMove.x * m_fSpeed, player.rb.velocity.y, m_vecMove.z * m_fSpeed);

            //Flip
            if (m_vecMove.x < 0)
            {
                player.playerStateMachine.CurrentState.FlipCharacter(false);
            }
            else if (m_vecMove.x > 0)
            {
                player.playerStateMachine.CurrentState.FlipCharacter(true);
            }
        }
        else if (player.isJoyStick == false && player.isAuto == false && player.isController == false || GameManager.Instance.playerController.isStunned)
        {
            player.rb.velocity = new Vector3(0, player.rb.velocity.y, 0);
        }
    }

    void OnTouch(Vector2 vecTouch)
    {
        Vector2 vec = new Vector2(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y);
        vec = Vector2.ClampMagnitude(vec, m_fRadius); // 벡터를 반지름 내로 제한
        m_rectJoystick.localPosition = vec;

        // X와 Z축 이동 벡터 계산
        m_vecMove = new Vector3(vec.x, 0f, vec.y).normalized;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!GameManager.Instance.playerController.isStunned)
        {
            OnTouch(eventData.position);
            player.isJoyStick = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.playerController.isStunned)
        {
            player.playerStateMachine.ChangeState(player.playerStateMachine.MoveState);
            AutoFalse();
            OnTouch(eventData.position);
            player.isJoyStick = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 원래 위치로 되돌립니다.
        m_rectJoystick.localPosition = Vector2.zero;
        player.isJoyStick = false;
        m_vecMove = Vector3.zero;
        if (player.isController == false)
        {
            player.targetSearch.TargetClear();
            player.playerStateMachine.ChangeState(player.playerStateMachine.IdleState);
        }            
    }

    public void AutoFalse()
    {
        player.isAuto = false;
        onImage.SetActive(false);
        offImage.SetActive(true);
        StopAutoSkillCoroutine();
    }
    public void AutoButtton()
    {
        player.isAuto = !player.isAuto;
        onImage.SetActive(player.isAuto);
        offImage.SetActive(!player.isAuto);

        if(!player.isAuto)
        {
            player.playerStateMachine.ChangeState(player.playerStateMachine.IdleState);
            StopAutoSkillCoroutine();
        }
        else
        {
            StartAutoSkillCoroutine();
        }
    }

    private void StartAutoSkillCoroutine()
    {
        if (autoSkillCoroutine == null)
        {
            autoSkillCoroutine = StartCoroutine(AutoUseSkillsCoroutine());
        }
    }

    private void StopAutoSkillCoroutine()
    {
        if (autoSkillCoroutine != null)
        {
            StopCoroutine(autoSkillCoroutine);
            autoSkillCoroutine = null;
        }
    }

    private IEnumerator AutoUseSkillsCoroutine()
    {
        while (player.isAuto)
        {
            if (GameManager.Instance.playerController.isStunned)
            {
                yield return new WaitUntil(() => !GameManager.Instance.playerController.isStunned);
            }

            yield return new WaitForSeconds(0.5f);

            int curSoulIndex = GameManager.Instance.player.PlayerSouls.SpawnIndex;

            if (IsAnySkillReady(curSoulIndex))
            {
                if (!skill1Button.isUses[curSoulIndex])
                    GameManager.Instance.playerController.UseSkill1();
                else if (!skill2Button.isUses[curSoulIndex])
                    GameManager.Instance.playerController.UseSkill2();
            }
            else
            {
                currentSoulIndex = (currentSoulIndex % 3) + 1; // 1, 2, 3 순환
                GameManager.Instance.playerController.SwitchSKill(currentSoulIndex);
                GameManager.Instance.joyStick.FindSkillButtons();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    // 스턴 해제 시 자동 모드 재시작 메서드
    public void ResumeAutoModeAfterStun()
    {
        if (player.isAuto)
        {
            StopAutoSkillCoroutine();
            StartAutoSkillCoroutine();
        }
    }

    private bool IsAnySkillReady(int soulIndex)
    {
        return !skill1Button.isUses[soulIndex] || !skill2Button.isUses[soulIndex];
    }
}
