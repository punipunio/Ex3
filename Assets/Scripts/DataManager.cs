using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System.Text;
using Unity.VisualScripting.FullSerializer;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; } // �V���O���g���C���X�^���X
    private List<string> csvData = new List<string>(); // �f�[�^��ێ�

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���ԂŔj������Ȃ��悤�ɂ���
        }
        else
        {
            Destroy(gameObject); // �d������C���X�^���X������Δj��
        }

        // CSV�w�b�_�[��ǉ��i����̂݁j
        if (csvData.Count == 0)
        {
            csvData.Add("SceneName,Timer1(ms),Timer2(ms),Timer1+Timer2(ms),Expressions,GeneratedText,T/F");
        }
    }

    // CSV�f�[�^�̍s��ǉ�
    public void AddCsvRow(string row)
    {
        csvData.Add(row);
    }

    // CSV�t�@�C���ɕۑ�
    public void SaveCsv(string filePath)
    {
        try
        {
            // BOM�t���̃G���R�[�f�B���O�Ńt�@�C�����쐬
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
