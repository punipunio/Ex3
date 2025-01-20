using UnityEngine;

public class RandomText : MonoBehaviour
{
    // 名前と接続詞の配列を用意
    private string[] names = { "たかし", "こういち", "しょうた" };
    private string[] connectors = { "は", "と", "に会った" };

    // 名前と接続詞の使用フラグ配列を用意
    private bool[] usedNames = { false, false, false };
    private bool[] usedConnectors = { false, false, false };

    // Start is called before the first frame update
    public string GenerateText()
    {
        // ランダムな文を生成して表示
        string result = GenerateRandomSentence();
        Debug.Log(result);  // Unityのコンソールに出力
        return result;
    }

    // ランダムな文を生成する関数
    private string GenerateRandomSentence()
    {
        string result = "";

        for (int i = 0; i < 3; i++)
        {
            int nameIdx;
            int connectorIdx;

            // 使用されていない名前をランダムに選択
            do
            {
                nameIdx = UnityEngine.Random.Range(0, 3); // UnityEngine.Random を使用
            } while (usedNames[nameIdx]); // 使用済みの名前が選ばれた場合は再選択
            usedNames[nameIdx] = true;  // 選択済みにマーク

            // 使用されていない接続詞をランダムに選択
            do
            {
                connectorIdx = UnityEngine.Random.Range(0, 3); // UnityEngine.Random を使用
            } while (usedConnectors[connectorIdx]); // 使用済みの接続詞が選ばれた場合は再選択
            usedConnectors[connectorIdx] = true;  // 選択済みにマーク

            // 選ばれた名前と接続詞を結果に追加（文節を区切るため「/」を使う）
            if (i != 2) result += names[nameIdx] + connectors[connectorIdx] + "/";
            else result += names[nameIdx] + connectors[connectorIdx] + "。" + "/";
        }

        return result; 
    }
}
