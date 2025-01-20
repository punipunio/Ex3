using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterPracticeManager : MonoBehaviour
{
    [SerializeField]
    private float timeToEndScene = 5f; // �V�[���I���܂ł̎��ԁi�b�j

    private float timer = 0f; // �o�ߎ��Ԃ�ǐ�

    void Update()
    {
        timer += Time.deltaTime; // ���Ԃ����Z

        if (timer >= timeToEndScene) // 7�b�o�߂�����
        {
            LoadNextScene(); // �V�[���I������
        }
    }

    private void LoadNextScene()
    {
        // �`���[�g���A���I����AMainController�ŊǗ�����ŏ��̃V�[���֑J��
        SceneManager.LoadScene(3); // �V�[��1�ɑJ�� (�r���h�ݒ�ŃC���f�b�N�X1���ŏ��̃V�[���Ɏw��)
    }
}
