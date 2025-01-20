//using UnityEngine;

//public class AnswerChecker : MonoBehaviour
//{
//    public GameObject sentenceChoices;  // ���͑I��UI
//    public GameObject emotionChoices;  // �\��I��UI

//    private AnswerData currentAnswer;
//    private System.Action<bool> onComplete;

//    public void SetupAnswerPhase(AnswerData answerData, System.Action<bool> callback)
//    {
//        currentAnswer = answerData;
//        onComplete = callback;

//        // �I��UI��\��
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
//        // �������킹���ʂ�UI��\����ɃR�[���o�b�N
//        sentenceChoices.SetActive(false);
//        emotionChoices.SetActive(false);
//        onComplete?.Invoke(isCorrect);
//    }
//}
