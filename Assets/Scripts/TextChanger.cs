using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{
    public RandomText randomText;
    public TextMeshPro ChangedText;

    void Start()
    {
        // RandomText から元のテキストを取得
        string originalText = randomText.GenerateText();

        // 抽出する位置と文字数
        int startIdx = 11; // 抽出開始位置（例として11を設定）
        int length = 7;    // 抽出する文字数（例として7を設定）

        // 範囲が正しいか確認
        if (startIdx < 0 || startIdx + length > originalText.Length)
        {
            Debug.LogError("抽出範囲が無効です。範囲を確認してください。");
            return;
        }

        // 透明なスペースでテキストを埋め、元の位置関係を保持
        string paddingStart = new string('\u2003', startIdx); // 開始位置分、文字幅に合った空白を挿入
        string extractedText = originalText.Substring(startIdx, length); // 抽出部分

        // ChangedTextに位置関係を保ったまま表示
        ChangedText.text = paddingStart + extractedText;
    }
}