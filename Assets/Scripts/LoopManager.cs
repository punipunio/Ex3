using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance;
    public int loopCount = 0; // ���݂̃��[�v��
    public int totalLoops = 3; // �ő僋�[�v��

    private void Awake()
    {
        // �V���O���g���C���X�^���X�̊Ǘ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���Ԃŕێ�
        }
        else if (Instance != this)
        {
            // �����̃C���X�^���X������ꍇ�A���̃C���X�^���X��j��
            Destroy(gameObject);
        }
    }

    public void ResetLoopManager()
    {
        totalLoops = 3;
    }
}
