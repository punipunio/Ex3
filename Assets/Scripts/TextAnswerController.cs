using UnityEngine;
using TMPro;

public class TextAnswerController : MonoBehaviour
{
    public GameObject[] textButtons; // 文章答え合わせ用のボタン
    private System.Action<bool> onComplete;
    private string[] answers = new string[4]; // 4つの選択肢を保持
    private int correctAnswerIndex; // 正解のインデックス

    public GenerateText generateText; // GenerateTextスクリプトへの参照

    public void ShowButtons()
    {
        // 文章生成
        answers[0] = generateText.ReturnAnswer(0); // 正解
        answers[1] = generateText.ReturnAnswer(1);
        answers[2] = generateText.ReturnAnswer(2);
        answers[3] = generateText.ReturnAnswer(3);
        correctAnswerIndex = 0; // 正解はReturnAnswer(0)

        // インデックスをランダムに並べ替える
        int[] indices = { 0, 1, 2, 3 };
        for (int i = indices.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        // ボタンにテキストを割り当て
        for (int i = 0; i < textButtons.Length; i++)
        {
            TMP_Text buttonText = textButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = answers[indices[i]];

            // ボタンのイベントを設定
            int answerIndex = indices[i];
            textButtons[i].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            textButtons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnAnswerSelected(answerIndex == correctAnswerIndex));
        }

        // ボタンを表示
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

