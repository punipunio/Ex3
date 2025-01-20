using UnityEngine;
using TMPro;

public class TextAnswerController : MonoBehaviour
{
    public GameObject[] textButtons; // ���͓������킹�p�̃{�^��
    private System.Action<bool> onComplete;
    private string[] answers = new string[4]; // 4�̑I������ێ�
    private int correctAnswerIndex; // �����̃C���f�b�N�X

    public GenerateText generateText; // GenerateText�X�N���v�g�ւ̎Q��

    public void ShowButtons()
    {
        // ���͐���
        answers[0] = generateText.ReturnAnswer(0); // ����
        answers[1] = generateText.ReturnAnswer(1);
        answers[2] = generateText.ReturnAnswer(2);
        answers[3] = generateText.ReturnAnswer(3);
        correctAnswerIndex = 0; // ������ReturnAnswer(0)

        // �C���f�b�N�X�������_���ɕ��בւ���
        int[] indices = { 0, 1, 2, 3 };
        for (int i = indices.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        // �{�^���Ƀe�L�X�g�����蓖��
        for (int i = 0; i < textButtons.Length; i++)
        {
            TMP_Text buttonText = textButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = answers[indices[i]];

            // �{�^���̃C�x���g��ݒ�
            int answerIndex = indices[i];
            textButtons[i].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            textButtons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnAnswerSelected(answerIndex == correctAnswerIndex));
        }

        // �{�^����\��
        foreach (GameObject button in textButtons)
        {
            button.SetActive(true);
        }
    }

    public void HideButtons()
    {
        foreach (GameObject button in textButtons)
        {
            button.SetActive(false);
        }
    }

    public void StartTextAnswerPhase(System.Action<bool> callback)
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

