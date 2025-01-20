using UnityEngine;
using UnityEngine.InputSystem;

public class SetObjectPosition : MonoBehaviour
{
    [Header("�I�u�W�F�N�g�̏����ʒu�Ɖ�]")]
    public Vector3 initialPosition = new Vector3(-45f, 0f, -48f);
    public Vector3 initialRotation = Vector3.zero;

    [Header("���Z�b�g�p�L�[")]
    public Key resetKey = Key.Enter;

    void Start()
    {
        // �����ʒu�Ɖ�]��ݒ�
        ResetTransform();
    }

    void Update()
    {
        // �w�肳�ꂽ�L�[�������ꂽ�ꍇ
        if (Keyboard.current != null && Keyboard.current[resetKey].wasPressedThisFrame)
        {
            ResetTransform();
            Debug.Log($"���݂̈ʒu: {transform.position}");
            Debug.Log($"���݂̉�]: {transform.rotation.eulerAngles}");
        }
    }

    private void ResetTransform()
    {
        // �ʒu�Ɖ�]��ݒ�
        transform.position = initialPosition;
        transform.rotation = Quaternion.Euler(initialRotation);

        // �f�o�b�O���O�Ŋm�F
        Debug.Log($"�ʒu�Ɖ�]�����Z�b�g���܂���:\n�ʒu: {transform.position}\n��]: {transform.rotation.eulerAngles}");
    }
}
