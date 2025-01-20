using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGazeController : MonoBehaviour
{
    public GameObject pointer; // �|�C���^�[�I�u�W�F�N�g
    public OVREyeGaze eyeGaze; // OVREyeGaze �R���|�[�l���g
    public float maxGazeDistance = 10.0f; // �����̍ő勗��
    public float smoothingSpeed = 10.0f; // �X���[�W���O�̑��x
    public Vector3 gazeOffset = new Vector3(0, 0, 0); // ���������̕␳�l

    void Start()
    {
        eyeGaze = GetComponent<OVREyeGaze>();   
    }

    void Update()
    {
        if (eyeGaze != null && eyeGaze.EyeTrackingEnabled)
        {
            Vector3 gazeOrigin = eyeGaze.transform.position; // �����̋N�_
            Vector3 gazeDirection = eyeGaze.transform.forward + gazeOffset; // �����̕����i�␳�l��������j

            // ���C�L���X�g�Ŏ������q�b�g�����ʒu���擾
            RaycastHit hit;
            Vector3 targetPosition;
            if (Physics.Raycast(gazeOrigin, gazeDirection, out hit, maxGazeDistance))
            {
                targetPosition = hit.point;
                pointer.transform.forward = hit.normal; // �|�C���^�[�̌�������
            }
            else
            {
                // �q�b�g���Ȃ��ꍇ�̃f�t�H���g�ʒu
                targetPosition = gazeOrigin + gazeDirection * maxGazeDistance;
                pointer.transform.forward = gazeDirection;
            }

            // �X���[�W���O��K�p���ă|�C���^�[���ړ�
            pointer.transform.position = Vector3.Lerp(pointer.transform.position, targetPosition, Time.deltaTime * smoothingSpeed);
        }
    }
}


