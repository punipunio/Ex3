using UnityEngine;

public class GenerateText : MonoBehaviour
{
    // ���̖��O���̃��X�g�ɕ���
    private string[] prefectures = { "�A�����J", "�t�����X", "�C�M���X", "�I�����_", "�l�p�[��",
                                     "�J�i�_", "�C���h", "�X�C�X", "�y���["};
    private string[] prefectures1 = { "�A�����J", "�t�����X", "�C�M���X", "�I�����_", "�l�p�[��" };
    private string[] prefectures2 = { "�J�i�_", "�C���h", "�X�C�X", "�y���[" };

    // �V�C�̔z����̃��X�g�ɕ���
    private string[] weathers = { "����", "�J" , "�܂�" };
    private string[] weathers1 = { "����", "�J" };
    private string[] weathers2 = { "�܂�" };

    private string randomPrefecture;
    private string randomWeather;
    private string randomPrefectureForAnswer;
    private string randomWeatherForAnswer;

    private string RandomTextGenerate()
    {
        // prefectures1 �� prefectures2 ��I��
        bool isPrefecture1 = Random.Range(0, 2) == 0;

        // �����_���ɓs���{����I��
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

        // ���𐶐�
        string sentence = $"{randomPrefecture}��{randomWeather}�B/";

        // �����_���ȓs���{���ƓV�C��S���X�g����I��
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
        if (n == 0) return randomPrefecture + "��" + randomWeather + "�B";
        else if (n == 1) return randomPrefecture + "��" + randomWeatherForAnswer + "�B";
        else if (n == 2) return randomPrefectureForAnswer + "��" + randomWeather + "�B";
        else if (n == 3) return randomPrefectureForAnswer + "��" + randomWeatherForAnswer + "�B";
        else return "";
    }
}

