using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;  // �V����Input System�̖��O���

public class TextUpdater : MonoBehaviour
{
    public TextMeshPro textObject; // �\������e�L�X�g�I�u�W�F�N�g
    private string[] textMessages = { "Message 1", "Message 2", "Message 3" }; // �\�����郁�b�Z�[�W�ꗗ
    private int currentIndex = 0; // ���݂̃��b�Z�[�W�C���f�b�N�X

    void Start()
    {
        // textMessages �z�񂪋�łȂ����Ƃ��m�F
        if (textMessages.Length == 0)
        {
            Debug.LogError("The textMessages array is empty!");
        }
        else
        {
            // �ŏ��̃��b�Z�[�W��\��
            textObject.text = textMessages[currentIndex];
        }
    }

    void Update()
    {
        // �V����Input System���g�p���āA�X�y�[�X�L�[�������ꂽ�����`�F�b�N
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            UpdateText();
        }
    }

    void UpdateText()
    {
        // �z�񂪋�łȂ��ꍇ�̂ݏ����𑱍s
        if (textMessages.Length > 0)
        {
            if (currentIndex < textMessages.Length - 1)
            {
                // ���̃��b�Z�[�W��\��
                currentIndex++;
                textObject.text = textMessages[currentIndex];
            }
            else
            {
                // �Ō�̃��b�Z�[�W���\�����ꂽ��A�X�y�[�X�L�[�Ńe�L�X�g������
                textObject.text = "";       
            }
        }
    }
}
