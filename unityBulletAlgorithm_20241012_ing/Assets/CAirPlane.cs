using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
�� �����ڵ�� 
źȯ ���� �˰��� �׽�Ʈ �ڵ��̴�. 

źȯ ���� 
    i) �Ϲ�źȯ: ���� �߿� źȯ�� ������ �̹� �����Ǿ� �ִ� źȯ
    ii) ����źȯ: ���� �߿� �߻� ������ ������ �����Ͽ� �����ϴ� źȯ
    iii) ����źȯ: ���� ������ �������� �߻�Ǵ� źȯ
�� �ִ�.

�� �������� �ۼ��� �� �˸� 
�ٸ� ��� źȯ������ �ۼ� �����ϴ�.

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

    //źȯ �߻� ��ƾ
    //      i) �߻���� ���� ����
    //      ii) źȯ�� �ӵ� ����
    //      iii) źȯ Ȱ��ȭ

    //�Ϲ�źȯ �߻�
    void DoFire()
    {
        Vector3 tPositionFire = Vector3.zero;
        tPositionFire = this.transform.position;

        Vector3 tVelocity = Vector3.zero;
        tVelocity = Vector3.up * 30f;//������ ��Į�����
        //'�ӵ�'�� �����߿� �̸� ������ źȯ

        CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
        tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);
        
        //���� �ٷ�� �������� ����, ������ �����̶� �ٸ���? ��ġ�� ������ �ٴ´�.
        //�������� �ٷ�� �����鿡 ǥ���� ������ ����.
        //  1m, 1kg, 1sec

        //ForceMode.Force    <-- �ð� ������ 1���̴�. ������ �״�� ������.   
        //ForceMode.Impulse     <-- �ð� ������ 1�������̴�. �� �����ӿ� �־��� ���� ��� ���Ѵ�.
    }

    //���� źȯ �߻�
    void DoFireAimed(Vector3 tPositionTarget)
    {
        Vector3 tPositionFire = Vector3.zero;
        tPositionFire = this.transform.position;

        Vector3 tVelocity = Vector3.zero;

        //��������
        //������ ũ���� ������  ���� = ��ǥ���� - ���� ����
        //������ ũ���� ������  ����<-- ����ȭ�Ͽ� ũ�Ⱑ 1�� ������ ���� ���͸� ���Ѵ�
        Vector3 tUnitVector = (tPositionTarget - tPositionFire);

        tVelocity = tUnitVector * 3f;//������ ��Į�� ����
        //'�ӵ�'�� �߻� ������ �����Ǵ� źȯ

        CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
        tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);
    }

    //���� źȯ
    //����ź�� �⺻������ ����ź�� ����
    //�ٸ� źȯ �� ������ ������ ���� �ð� �������� �������ϴ� ���̴�


    //���� źȯ �߻�
    void DoFireCircled()
    {
        float tAngle = 0f;
        for(int ti = 0; ti <8; ti++ )
        {
            Vector3 tPositionFire = Vector3.zero;
            tPositionFire = this.transform.position;

            Vector3 tVelocity = Vector3.zero;

            //��ī��Ʈ ��ǥ���� ���� ���а� �� ��ǥ���� �������а��� ����
            // x = r*cosT
            //y = r*sinT

            //������ ����
            //degree �� : �ѹ����� 360��� �� �� �� �ϳ��� 1�ζ�� ����
            //radian ȣ��: �������� r�� ���� ���� �߿� ���̰� r�� ȣ
            //          <-- �Ǽ��� ���� ü�迡 �����ϱ� ������ �����Լ��� ���ӿ��������� radian�� ��꿡 ����Ѵ�

            // 360��:1�� = 2*Pl : x
            //x = PI/ 180 <-- degree to radian, Mathf.Deg2Red


            //ũ�Ⱑ 1�� ������ ���⺤�͸� ���ϱ� ���� r�� 1
            tVelocity.z = 0f;
            tVelocity.x = 1f * Mathf.Cos(tAngle * Mathf.Deg2Rad);
            tVelocity.y = 1f * Mathf.Sin(tAngle * Mathf.Deg2Rad);

            tAngle += 45f;
            tVelocity = tVelocity * 30f;//�ӷ��� ��Į�� ����

            CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
            tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);
        }
    }
}
