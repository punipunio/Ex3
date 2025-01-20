using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����؂�ւ���Ă��j�����Ȃ�
        }
        else
        {
            Destroy(gameObject); // �d����h�����ߊ�����BGMManager������ꍇ�͍폜
        }
    }
}
