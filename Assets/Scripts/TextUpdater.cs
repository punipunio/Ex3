using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;  // 新しいInput Systemの名前空間

public class TextUpdater : MonoBehaviour
{
    public TextMeshPro textObject; // 表示するテキストオブジェクト
    private string[] textMessages = { "Message 1", "Message 2", "Message 3" }; // 表示するメッセージ一覧
    private int currentIndex = 0; // 現在のメッセージインデックス

    void Start()
    {
        // textMessages 配列が空でないことを確認
        if (textMessages.Length == 0)
        {
            Debug.LogError("The textMessages array is empty!");
        }
        else
        {
            // 最初のメッセージを表示
            textObject.text = textMessages[currentIndex];
        }
    }

    void Update()
    {
        // 新しいInput Systemを使用して、スペースキーが押されたかをチェック
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            UpdateText();
        }
    }

    void UpdateText()
    {
        // 配列が空でない場合のみ処理を続行
        if (textMessages.Length > 0)
        {
            if (currentIndex < textMessages.Length - 1)
            {
                // 次のメッセージを表示
                currentIndex++;
                textObject.text = textMessages[currentIndex];
            }
            else
            {
                // 最後のメッセージが表示された後、スペースキーでテキストを消去
                textObject.text = "";       
            }
        }
    }
}
