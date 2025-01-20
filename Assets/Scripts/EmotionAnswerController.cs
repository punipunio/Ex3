using UnityEngine;
using TMPro;

public class EmotionAnswerController : MonoBehaviour
{
    public GameObject[] emotionButtons; // �\������킹�p�̃{�^�� (�Œ�z�u: �^��E�Ί�E�߂��݁E�{��)
    private System.Action<bool> onComplete;
    private string[] emotions = {"�^��", "�Ί�", "�߂���"};
    private int correctAnswerIndex; // �����̃C���f�b�N�X
    public BackgroundShape backgroundShape;

    public void ShowButtons()
    {

        // �����̕\��
        correctAnswerIndex = backgroundShape.CorrectAnswer();

        int[] indies = { 0, 1, 2};

        // �{�^���Ƀe�L�X�g�����蓖��
        for (int i = 0; i < emotionButtons.Length; i++)
        {
            TMP_Text buttonText = emotionButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = emotions[i]; // �Œ肳�ꂽ�z�u�̕\��e�L�X�g

            int answerIndex = indies[i];
            // �{�^���̃C�x���g��ݒ�
            emotionButtons[i].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            emotionButtons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnAnswerSelected(answerIndex == correctAnswerIndex));
        }

        // �{�^����\��
        foreach (GameObject button in emotionButtons)
        {
            button.SetActive(true);
        }
    }

    public void HideButtons()
    {
        foreach (GameObject button in emotionButtons)
        {
            button.SetActive(false);
        }
    }

    public void StartEmotionAnswerPhase(System.Action<bool> callback)
    {
        onComplete = callback;
        ShowButtons();
    }

    private void OnAnswerSelected(bool isCorrect)
    {
        HideButtons();
        onComplete?.Invoke(isCorrect);
    }
}

