using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSetup : MonoBehaviour
{
    public Camera mainCamera; // ���C���J�������w��

    void Start()
    {
        // ���C���J�����������擾�i���ݒ�̏ꍇ�j
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }     

        // �J�����̌�����Z���������ɐݒ�
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0); // ��]�p�x���w��i0, 0, 0��Z���������j
    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame) 
        {
            // �J�����̌�����Z���������ɐݒ�
            mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0); // ��]�p�x���w��i0, 0, 0��Z���������j
        }
    }
}
