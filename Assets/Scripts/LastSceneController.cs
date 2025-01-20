using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    public LoopManager loopManager;
    public TextMeshPro sentence;

    // �V�[�����[�v�Ǘ�
    private int sceneStartIndex = 3; // �V�[��1�̃C���f�b�N�X

    [SerializeField]
    private float timeToEndScene = 3f; // �V�[���I���܂ł̎��ԁi�b�j

    private float timer = 0f; // �o�ߎ��Ԃ�ǐ�

    void Update()
    {
        timer += Time.deltaTime; // ���Ԃ����Z

        SetController();
    }

    private void QuitGame()
    {
        UnityEngine.Debug.Log("This is the last scene.");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void SetController() 
    {
        string result = sentence.text;

        if(LoopManager.Instance.loopCount < LoopManager.Instance.totalLoops) 
        {
            sentence.text = $"{LoopManager.Instance.loopCount}�Z�b�g�ڂ��I�����܂����B\n�c��{LoopManager.Instance.totalLoops - LoopManager.Instance.loopCount}�Z�b�g�ł��B";
            if (timer >= timeToEndScene)
            {
                timer = 0f;
                SceneManager.LoadScene(sceneStartIndex);
            }
        }
        else if(LoopManager.Instance.loopCount == LoopManager.Instance.totalLoops) 
        {
            sentence.text = $"�����I���ł��B\n�����͂��肪�Ƃ��������܂����B";
            if (timer >= timeToEndScene)
            {
                QuitGame(); // �V�[���I������
            }
        }
    }
}
