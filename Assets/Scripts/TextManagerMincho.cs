using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TextManagerMincho : MonoBehaviour
{
    public TextMeshPro textObject;  // 文を表示するテキストオブジェクト
    //public RandomText randomText;
    public GenerateText generateText;
    private string[] messageSegments;  // 文節の配列
    private int currentSegmentIndex = 0;  // 現在の文節インデックス
    private int start, length, i;
    public string originalText;

    // テキストを設定し、文節ごとに分割するメソッド
    void Start()
    {
        originalText = generateText.GenerateSentence();
        // 「/」で文節を分割
        messageSegments = originalText.Split('/');
        currentSegmentIndex = 0;  // インデックスの初期化
        start = 0;
        length = 0;
        i = 0;
    }

    // 次の文節を表示するメソッド
    public void ShowNextSegment()
    {
        if (currentSegmentIndex < messageSegments.Length - 1)
        {
            while (i <= originalText.Length)
            {

                if (originalText[i] == '/') //一文字ずつチェックし、'/'があった場合文字を表示する
                {
                    length = i - start;
                    //UnityEngine.Debug.Log("i="+i+", start="+start+", length=" + length);             
                    string paddingStart = new string('　', start);
                    string adjustedPadding = paddingStart.Replace("　", "<size=90%>　</size>");
                    string extractedText = originalText.Substring(start, length);
                    textObject.text = adjustedPadding + extractedText;
                    currentSegmentIndex++;
                    start = i + 1;
                    i++;
                    //UnityEngine.Debug.Log("currentSegmentIndext=" + currentSegmentIndex + "messageSegments.Length=" + messageSegments.Length);
                    break;
                }

                i++;
            }
        }
    }

    // テキストを非表示にするメソッド
    public void HideText()
    {
        textObject.text = "";  // テキストを非表示
    }

    // 追加されたメソッド: 次の文節があるかをチェックする
    public bool HasNextMessage()
    {
        return currentSegmentIndex < messageSegments.Length - 1;
    }
}

