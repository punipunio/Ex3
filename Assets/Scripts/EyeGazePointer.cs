using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class EyeGazePointer : MonoBehaviour
{
    public GameObject pointerObject; // �|�C���^�[�Ƃ��Ďg���I�u�W�F�N�g
    public float maxDistance = 5.0f; // ������Ǐ]����ő勗��
    public LayerMask collisionLayer; // �Փ˂����o���郌�C���[�}�X�N

    private InputDevice eyeGazeDevice;
    private InputControl poseControl;


    void Start()
    {
        // Eye Gaze �f�o�C�X������
        foreach (var device in InputSystem.devices)
        {
            if (device.layout == "EyeGaze")
            {
                eyeGazeDevice = device;
                poseControl = eyeGazeDevice["pose"];
                Debug.Log("Eye Gaze device found.");
                break;
            }
        }

        if (eyeGazeDevice == null)
        {
            Debug.LogError("No Eye Gaze device found.");
        }
    }

    void Update()
    {
        if (eyeGazeDevice != null && poseControl != null && poseControl is InputControl<PoseState> poseStateControl)
        {
            PoseState poseState = poseStateControl.ReadValue();

            if (poseState.isTracked)
            {
                Vector3 gazeOrigin = poseState.position;
                Vector3 gazeDirection = poseState.rotation * Vector3.forward;

                // �Փ˂����o
                if (Physics.Raycast(gazeOrigin, gazeDirection, out RaycastHit hit, maxDistance, collisionLayer))
                {
                    // �������Փ˂����ʒu�Ƀ|�C���^�[���ړ�
                    pointerObject.transform.position = hit.point;
                }
                else
                {
                    // �Փ˂��Ȃ��ꍇ�͎����̕����Ƀ|�C���^�[���ړ�
                    pointerObject.transform.position = gazeOrigin + gazeDirection * maxDistance;
                }

                // �|�C���^�[���A�N�e�B�u��
                if (!pointerObject.activeSelf)
                {
                    pointerObject.SetActive(true);
                }
            }
            if (poseState.isTracked)
            {
                Debug.Log($"Gaze Origin: {poseState.position}, Gaze Direction: {poseState.rotation * Vector3.forward}");
            }
            else
            {
                Debug.Log("Eye gaze is not being tracked.");
            }

        }
        else
        {
            // �f�o�C�X�������ȏꍇ�A�|�C���^�[���\���ɂ���
            if (pointerObject.activeSelf)
            {
                pointerObject.SetActive(false);
            }
        }
    }
}

