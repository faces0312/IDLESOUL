using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform m_rectBack; // ���̽�ƽ ���
    public RectTransform m_rectJoystick; // ���̽�ƽ ���
    public Player player; // �÷��̾��� Rigidbody
    public float m_fRadius; // ���̽�ƽ ����� ������
    public float m_fSpeed = 5.0f; // �̵� �ӵ�
    private Vector3 m_vecMove; // �̵� ����
    private bool m_bTouch = false; // ��ġ ����

    [Header("Auto")]
    public GameObject onImage;
    public GameObject offImage;

    void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
        m_fRadius = m_rectBack.rect.width * 0.5f;
    }

    void Start()
    {
        AutoButtton();
    }

    void FixedUpdate()
    {
        if (m_bTouch)
        {
            player.rb.velocity = new Vector3(m_vecMove.x * m_fSpeed, player.rb.velocity.y, m_vecMove.z * m_fSpeed);

            //Flip
            /*if (m_vecMove.x < 0)
            {
                
            }
            else if (m_vecMove.x > 0)
            {
                
            }*/
        }
        else if(m_bTouch == false && player.isAuto == false)
        {
            player.rb.velocity = new Vector3(0, player.rb.velocity.y, 0);
        }
    }

    void OnTouch(Vector2 vecTouch)
    {
        Vector2 vec = new Vector2(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y);
        vec = Vector2.ClampMagnitude(vec, m_fRadius); // ���͸� ������ ���� ����
        m_rectJoystick.localPosition = vec;

        // X�� Z�� �̵� ���� ���
        m_vecMove = new Vector3(vec.x, 0f, vec.y).normalized;
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AutoFalse();
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ���� ��ġ�� �ǵ����ϴ�.
        m_rectJoystick.localPosition = Vector2.zero;
        m_bTouch = false;
        m_vecMove = Vector3.zero;
        player.playerStateMachine.ChangeState(player.playerStateMachine.IdleState);
        player.targetSearch.TargetClear();
        player.targetSearch.OnTargetSearch();
    }

    public void AutoButtton()
    {
        player.isAuto = !player.isAuto;
        onImage.SetActive(player.isAuto);
        offImage.SetActive(!player.isAuto);
    }

    public void AutoFalse()
    {
        if (player.isAuto == true)
            AutoButtton();
    }
}
