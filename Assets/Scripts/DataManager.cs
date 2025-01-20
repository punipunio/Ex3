using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System.Text;
using Unity.VisualScripting.FullSerializer;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; } // シングルトンインスタンス
    private List<string> csvData = new List<string>(); // データを保持

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン間で破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 重複するインスタンスがあれば破棄
        }

        // CSVヘッダーを追加（初回のみ）
        if (csvData.Count == 0)
        {
            csvData.Add("SceneName,Timer1(ms),Timer2(ms),Timer1+Timer2(ms),Expressions,GeneratedText,T/F");
        }
    }

    // CSVデータの行を追加
    public void AddCsvRow(string row)
    {
        csvData.Add(row);
    }

    // CSVファイルに保存
    public void SaveCsv(string filePath)
    {
        try
        {
            // BOM付きのエンコーディングでファイルを作成
            using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
            {
                foreach (string row in csvData)
                {
                    writer.WriteLine(row);
                }
            }
            Debug.Log($"CSV saved at: {filePath}");
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to save CSV: {e.Message}");
        }
    }
}
