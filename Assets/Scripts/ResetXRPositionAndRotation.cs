using UnityEngine;
using UnityEngine.InputSystem;

public class ResetXRPositionAndRotation : MonoBehaviour
{
    public Transform xrOrigin;  // XR Origin��Transform
    public Transform mainCamera; // Main Camera��Transform

    public Transform targetObject;  // xrOrigin���ړ�������^�[�Q�b�g�I�u�W�F�N�g

    public Vector3 targetDirection = new Vector3(0f, 0f, -1f);  // ���Z�b�g��Ɍ��������������i�ݒ�\�j

    private Vector3 initialPosition; // �����ʒu��ێ�����ϐ�

    void Start()
    {
        // �����ʒu��ݒ�i�ʒu�̃��Z�b�g�͈�x�����s���j
        if (xrOrigin != null)
        {
            initialPosition = xrOrigin.position;  // �����ʒu��ێ�
        }
    }

    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame || InputManagerLR.SecondaryButtonL_OnPress())
        {
            // 'Enter'�L�[�܂��͎w�肵���{�^���������ꂽ�Ƃ��Ɍ��������Z�b�g����
            ResetRotation();
        }
    }

    // ���݂̃J�����̌����ƃ��Z�b�g��̕����̊p�x�����v�Z���A��]��K�p
    void ResetRotation()
    {
        if (xrOrigin != null && mainCamera != null)
        {
            // XR Origin���^�[�Q�b�g�I�u�W�F�N�g�̈ʒu�Ɉړ�
            if (targetObject != null)
            {
                xrOrigin.position = targetObject.position;  // �^�[�Q�b�g�I�u�W�F�N�g�̈ʒu�Ɉړ�
            }

            // ���݂̃J�����̌����i�J������forward�����j
            Vector3 currentDirection = mainCamera.forward;
            currentDirection.y = 0f;  // ���������̌����������g�p

            // ���������������i�ݒ肳�ꂽtargetDirection�j
            Vector3 desiredDirection = targetDirection.normalized;

            // ���݂̕����Ǝw�肵�������̊Ԃ̊p�x���v�Z
            float angle = Vector3.SignedAngle(currentDirection, desiredDirection, Vector3.up);

            // ��]��K�p�i�p�x���w������ɉ�]������j
            xrOrigin.Rotate(Vector3.up, angle, Space.World);  // �������iy���j����ɉ�]������
        }
    }
}
