using UnityEngine;
using TMPro;

public class EmotionAnswerController : MonoBehaviour
{
    public GameObject[] emotionButtons; // 表情答え合わせ用のボタン (固定配置: 真顔・笑顔・悲しみ・怒り)
    private System.Action<bool> onComplete;
    private string[] emotions = {"真顔", "笑顔", "悲しみ"};
    private int correctAnswerIndex; // 正解のインデックス
    public BackgroundShape backgroundShape;

    public void ShowButtons()
    {

        // 正解の表情
        correctAnswerIndex = backgroundShape.CorrectAnswer();

        int[] indies = { 0, 1, 2};

        // ボタンにテキストを割り当て
        for (int i = 0; i < emotionButtons.Length; i++)
        {
            TMP_Text buttonText = emotionButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = emotions[i]; // 固定された配置の表情テキスト

            int answerIndex = indies[i];
            // ボタンのイベントを設定
            emotionButtons[i].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            emotionButtons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnAnswerSelected(answerIndex == correctAnswerIndex));
        }

        // ボタンを表示
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

