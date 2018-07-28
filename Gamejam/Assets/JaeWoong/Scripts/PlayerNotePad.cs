using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNotePad : MonoBehaviour
{
    public static PlayerNotePad instance;

    public Text comboTimeText;
    public Text comboText;
    public Building buildingInfo;

    public int touchNum;
    public int nowCombo;

    public float comboTime;

    public bool canTouch;
    public bool isChkComTim;

    void Awake()
    {
        instance = this;

        canTouch = true;
        isChkComTim = false;

        nowCombo = 0;

        comboTime = 0.0f;
    }

    void Update()
    {
        Debug.Log(nowCombo);
        comboText.text = "Combo : " + nowCombo.ToString();
        comboTimeText.text = "Time : " + comboTime.ToString();
    }

    public void TouchL1Pad()//왼쪽 위 노트를 눌렀을 때
    {
        if (!canTouch)//터치가 불가능한 상황이면 리턴
            return;

        touchNum = 1;

        CheckCombo();
    }
    public void TouchR1Pad()//오른쪽 위 노트를 눌렀을 때
    {
        if (!canTouch)//터치가 불가능한 상황이면 리턴
            return;

        touchNum = 2;

        CheckCombo();
    }
    public void TouchL2Pad()//왼쪽 아래 노트를 눌렀을 때
    {
        if (!canTouch)//터치가 불가능한 상황이면 리턴
            return;

        touchNum = 3;

        CheckCombo();
    }
    public void TouchR2Pad()//오른쪽 아래 노트를 눌렀을 때
    {
        if (!canTouch)//터치가 불가능한 상황이면 리턴
            return;
        
        touchNum = 4;

        CheckCombo();
    }
    //스킬키 누르면 canTouch = true; nowCombo = 0; comboTime = 1.0f; StartCoroutine("CheckComboTime");
    public void CheckCombo()
    {
        if (buildingInfo == null)//만약 건물이 생성 되어 있지 않다면
        {
            Debug.Log("sorry buildinginfo is null");
            return;
        }
        if (touchNum != buildingInfo.notePattern[nowCombo])//만약 콤보를 실패시켰다면
        {
            StartCoroutine("TouchFails");
            StopCoroutine("CheckComboTime");

            return;
        }
        else//만약 콤보를 성공시켰다면
        {
            if ((nowCombo + 1).Equals(buildingInfo.maxPatternNum))//만약 콤보를 완성시켰다면
            {
                Destroy(buildingInfo.gameObject);//건물을 삭제 시킨다.

                StopCoroutine("CheckComboTime");//콤보타임을 스탑 시킨다
                isChkComTim = false;
                comboTime = 0.0f;
                nowCombo = 0;

                return;
            }

            comboTime = 1f;
            nowCombo++;

            if (!isChkComTim)//코루틴 중복 실행 방지
                StartCoroutine("CheckComboTime");
        }
    }

    IEnumerator CheckComboTime()//1초 내에 콤보를 실행 하는 지 여부를 체크하는 함수
    {
        isChkComTim = true;

        for (; comboTime >= 0.0f; comboTime -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //1초 내에 콤보를 실행 하지 못했을때
        nowCombo = 0;//현재 콤보 0으로
        isChkComTim = false;//다시 이 코루틴을 실행 시킬 수 있게 false로 바꿔줌
    }

    IEnumerator TouchFails()//콤보에 틀렸을때 실행되는 함수
    {
        nowCombo = 0;//현재 콤보 0으로
        comboTime = 0.0f;

        isChkComTim = false;
        canTouch = false;
        yield return new WaitForSeconds(1f);//사실 콤보 틀리면 1초 후에 콤보를 다시 입력할 수 있게 1초 후에 canTouch = true를 해줬지만 이제는 콤보를 실패시키면 다시 스킬부터 선택해야 하기 때문에 아래 구문은 의미가 없다
        canTouch = true;
    }
}
