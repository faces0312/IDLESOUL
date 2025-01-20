using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICursor : SingletonDDOL<UICursor>
{
    [SerializeField] private RectTransform cursorTrans;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        CursorMoving();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
        }
    }

    private void CursorMoving()
    {
        // ���� ĵ���� ũ�⸦ ����� ���콺 ��ǥ ��ȯ
        Vector2 mousePosition = Input.mousePosition;

        float canvasWidth = GetCanvasWidth();
        float canvasHeight = GetCanvasHeight();

        // ���콺 ��ġ�� ���� ĵ���� �������� ��ȯ
        float x = (mousePosition.x / Screen.width) * canvasWidth - (canvasWidth / 2);
        float y = (mousePosition.y / Screen.height) * canvasHeight - (canvasHeight / 2);

        cursorTrans.localPosition = new Vector2(x, y);

        // ���콺 ���α�
        float tmp_cursorPosX = cursorTrans.localPosition.x;
        float tmp_cursorPosY = cursorTrans.localPosition.y;

        float min_width = -canvasWidth / 2;
        float max_width = canvasWidth / 2;
        float min_height = -canvasHeight / 2;
        float max_height = canvasHeight / 2;
        int padding = 20;

        tmp_cursorPosX = Mathf.Clamp(tmp_cursorPosX, min_width + padding, max_width - padding);
        tmp_cursorPosY = Mathf.Clamp(tmp_cursorPosY, min_height + padding, max_height - padding);

        cursorTrans.localPosition = new Vector2(tmp_cursorPosX, tmp_cursorPosY);
    }

    // ���� ĵ������ ���� �ʺ� ��������
    private float GetCanvasWidth()
    {
        return Screen.width * (1.0f / Screen.dpi) * 96.0f; // dpi�� ����Ͽ� ��ȯ
    }

    // ���� ĵ������ ���� ���� ��������
    private float GetCanvasHeight()
    {
        return Screen.height * (1.0f / Screen.dpi) * 96.0f;
    }

    //private void CursorMoving()
    //{
    //    float x = Input.mousePosition.x - (Screen.width / 2);
    //    float y = Input.mousePosition.y - (Screen.height / 2);
    //    cursorTrans.localPosition = new Vector2(x, y);

    //    // ���콺 ���α�
    //    float tmp_cursorPosX = cursorTrans.localPosition.x;
    //    float tmp_cursorPosY = cursorTrans.localPosition.y;

    //    float min_width = -Screen.width / 2;
    //    float max_width = Screen.width / 2;
    //    float min_height = -Screen.height / 2;
    //    float max_height = Screen.height / 2;
    //    int padding = 20;

    //    tmp_cursorPosX = Mathf.Clamp(tmp_cursorPosX, min_width + padding, max_width - padding);
    //    tmp_cursorPosY = Mathf.Clamp(tmp_cursorPosY, min_height + padding, max_height - padding);

    //    cursorTrans.localPosition = new Vector2(tmp_cursorPosX, tmp_cursorPosY);
    //}
}
