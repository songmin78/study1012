using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
이 예시코드는 
탄환 궤적 알고리즘 테스트 코드이다. 

탄환 에는 
    i) 일반탄환: 제작 중에 탄환의 방향이 이미 결정되어 있는 탄환
    ii) 조준탄환: 실행 중에 발사 시점에 방향을 조준하여 결정하는 탄환
    iii) 원형탄환: 원형 형태의 방향으로 발사되는 탄환
이 있다.

이 세가지를 작성할 줄 알면 
다른 모든 탄환궤적도 작성 가능하다.

*/

public class CAirPlane : MonoBehaviour
{
    [SerializeField]
    CBullet PFBullet = null;

    [SerializeField] GameObject mTargetObject = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            //DoFire();
            //DoFireAimed(mTargetObject.transform.position);
            DoFireCircled();
        }
    }

    //탄환 발사 루틴
    //      i) 발사시작 지점 설정
    //      ii) 탄환의 속도 설정
    //      iii) 탄환 활성화

    //일반탄환 발사
    void DoFire()
    {
        Vector3 tPositionFire = Vector3.zero;
        tPositionFire = this.transform.position;

        Vector3 tVelocity = Vector3.zero;
        tVelocity = Vector3.up * 30f;//벡터의 스칼라곱셈
        //'속도'가 제작중에 미리 지정된 탄환

        CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
        tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);
        
        //수를 다루는 관점에서 보면, 물리가 수학이란 다른점? 수치에 단위가 붙는다.
        //물리에서 다루는 단위들에 표준은 다음과 같다.
        //  1m, 1kg, 1sec

        //ForceMode.Force    <-- 시간 단위가 1초이다. 물리를 그대로 따른다.   
        //ForceMode.Impulse     <-- 시간 단위가 1프레임이다. 한 프레임에 주어진 힘을 모두 가한다.
    }

    //조준 탄환 발사
    void DoFireAimed(Vector3 tPositionTarget)
    {
        Vector3 tPositionFire = Vector3.zero;
        tPositionFire = this.transform.position;

        Vector3 tVelocity = Vector3.zero;

        //단위백터
        //임의의 크기의 방향의  백터 = 목표기점 - 시작 지점
        //임의의 크기의 방향의  백터<-- 정규화하여 크기가 1인 순수한 방향 백터를 구한다
        Vector3 tUnitVector = (tPositionTarget - tPositionFire);

        tVelocity = tUnitVector * 3f;//백터의 스칼라 곱셈
        //'속도'가 발사 시점에 결정되는 탄환

        CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
        tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);
    }

    //유도 탄환
    //유도탄은 기본적으로 조준탄과 같다
    //다만 탄환 이 스스로 방향을 일정 시간 간격으로 재조준하는 것이다


    //원형 탄환 발사
    void DoFireCircled()
    {
        float tAngle = 0f;
        for(int ti = 0; ti <8; ti++ )
        {
            Vector3 tPositionFire = Vector3.zero;
            tPositionFire = this.transform.position;

            Vector3 tVelocity = Vector3.zero;

            //데카르트 좌표계의 구성 성분과 극 좌표계의 구성성분과의 관계
            // x = r*cosT
            //y = r*sinT

            //각도의 개념
            //degree 도 : 한바퀴를 360등분 한 것 중 하나를 1로라고 하자
            //radian 호도: 반지름이 r인 원의 원주 중에 길이가 r인 호
            //          <-- 실수의 연산 체계에 적합하기 때문에 수학함수나 게임엔진에서는 radian를 계산에 사용한다

            // 360도:1도 = 2*Pl : x
            //x = PI/ 180 <-- degree to radian, Mathf.Deg2Red


            //크기가 1인 순수한 방향벡터를 구하기 위해 r은 1
            tVelocity.z = 0f;
            tVelocity.x = 1f * Mathf.Cos(tAngle * Mathf.Deg2Rad);
            tVelocity.y = 1f * Mathf.Sin(tAngle * Mathf.Deg2Rad);

            tAngle += 45f;
            tVelocity = tVelocity * 30f;//속력을 스칼라 곱셈

            CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
            tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);
        }
    }
}
