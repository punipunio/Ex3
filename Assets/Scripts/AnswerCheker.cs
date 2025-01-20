//using UnityEngine;

//public class AnswerChecker : MonoBehaviour
//{
//    public GameObject sentenceChoices;  // 文章選択UI
//    public GameObject emotionChoices;  // 表情選択UI

//    private AnswerData currentAnswer;
//    private System.Action<bool> onComplete;

//    public void SetupAnswerPhase(AnswerData answerData, System.Action<bool> callback)
//    {
//        currentAnswer = answerData;
//        onComplete = callback;

//        // 選択UIを表示
//        sentenceChoices.SetActive(true);
//        emotionChoices.SetActive(true);
//    }

//    public void OnSentenceSelected(string selectedSentence)
//    {
//        bool isCorrect = selectedSentence == currentAnswer.CorrectSentence;
//        EvaluateAnswer(isCorrect);
//    }

//    public void OnEmotionSelected(string selectedEmotion)
//    {
//        bool isCorrect = selectedEmotion == currentAnswer.CorrectEmotion;
//        EvaluateAnswer(isCorrect);
//    }

//    private void EvaluateAnswer(bool isCorrect)
//    {
//        // 答え合わせ結果をUI非表示後にコールバック
//        sentenceChoices.SetActive(false);
//        emotionChoices.SetActive(false);
//        onComplete?.Invoke(isCorrect);
//    }
//}
