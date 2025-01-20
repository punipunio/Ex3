using UnityEngine;

public class GenerateText : MonoBehaviour
{
    // 国の名前を二つのリストに分割
    private string[] prefectures = { "アメリカ", "フランス", "イギリス", "オランダ", "ネパール",
                                     "カナダ", "インド", "スイス", "ペルー"};
    private string[] prefectures1 = { "アメリカ", "フランス", "イギリス", "オランダ", "ネパール" };
    private string[] prefectures2 = { "カナダ", "インド", "スイス", "ペルー" };

    // 天気の配列を二つのリストに分割
    private string[] weathers = { "晴れ", "雨" , "曇り" };
    private string[] weathers1 = { "晴れ", "雨" };
    private string[] weathers2 = { "曇り" };

    private string randomPrefecture;
    private string randomWeather;
    private string randomPrefectureForAnswer;
    private string randomWeatherForAnswer;

    private string RandomTextGenerate()
    {
        // prefectures1 と prefectures2 を選択
        bool isPrefecture1 = Random.Range(0, 2) == 0;

        // ランダムに都道府県を選択
        if (isPrefecture1)
        {
            randomPrefecture = prefectures1[Random.Range(0, prefectures1.Length)];
            randomWeather = weathers1[Random.Range(0, weathers1.Length)];
        }
        else
        {
            randomPrefecture = prefectures2[Random.Range(0, prefectures2.Length)];
            randomWeather = weathers2[Random.Range(0, weathers2.Length)];
        }

        // 文を生成
        string sentence = $"{randomPrefecture}は{randomWeather}。/";

        // ランダムな都道府県と天気を全リストから選択
        while (true)
        {
            randomPrefectureForAnswer = prefectures[Random.Range(0, prefectures.Length)];
            if (randomPrefectureForAnswer != randomPrefecture) break;
        }
        while (true)
        {
            randomWeatherForAnswer = weathers[Random.Range(0, weathers.Length)];
            if (randomWeatherForAnswer != randomWeather) break;
        }

        return sentence;
    }

    public string GenerateSentence()
    {
        string result = RandomTextGenerate();
        Debug.Log(result);
        return result;
    }

    public string ReturnAnswer(int n)
    {    
        if (n == 0) return randomPrefecture + "は" + randomWeather + "。";
        else if (n == 1) return randomPrefecture + "は" + randomWeatherForAnswer + "。";
        else if (n == 2) return randomPrefectureForAnswer + "は" + randomWeather + "。";
        else if (n == 3) return randomPrefectureForAnswer + "は" + randomWeatherForAnswer + "。";
        else return "";
    }
}

