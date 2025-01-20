using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{

    private int buttonPressCount = 0;

    private void Update()
    {
        if (InputManagerLR.PrimaryButtonR_OnPress() ||
            InputManagerLR.PrimaryButtonL_OnPress() ||
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            buttonPressCount++; // �{�^���������ꂽ�񐔂��J�E���g

            if (buttonPressCount >= 5) // 5�񉟂��ꂽ�ꍇ
            {
                LoadFirstScene(); // �V�[�������[�h
                buttonPressCount = 0; // �J�E���g�����Z�b�g
            }
        }
    }

    private void LoadFirstScene()
    {
        // �`���[�g���A���I����AMainController�ŊǗ�����ŏ��̃V�[���֑J��
        SceneManager.LoadScene(1); // �V�[��1�ɑJ�� (�r���h�ݒ�ŃC���f�b�N�X1���ŏ��̃V�[���Ɏw��)
    }
}
