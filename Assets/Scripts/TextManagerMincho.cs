using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TextManagerMincho : MonoBehaviour
{
    public TextMeshPro textObject;  // ����\������e�L�X�g�I�u�W�F�N�g
    //public RandomText randomText;
    public GenerateText generateText;
    private string[] messageSegments;  // ���߂̔z��
    private int currentSegmentIndex = 0;  // ���݂̕��߃C���f�b�N�X
    private int start, length, i;
    public string originalText;

    // �e�L�X�g��ݒ肵�A���߂��Ƃɕ������郁�\�b�h
    void Start()
    {
        originalText = generateText.GenerateSentence();
        // �u/�v�ŕ��߂𕪊�
        messageSegments = originalText.Split('/');
        currentSegmentIndex = 0;  // �C���f�b�N�X�̏�����
        start = 0;
        length = 0;
        i = 0;
    }

    // ���̕��߂�\�����郁�\�b�h
    public void ShowNextSegment()
    {
        if (currentSegmentIndex < messageSegments.Length - 1)
        {
            while (i <= originalText.Length)
            {

                if (originalText[i] == '/') //�ꕶ�����`�F�b�N���A'/'���������ꍇ������\������
                {
                    length = i - start;
                    //UnityEngine.Debug.Log("i="+i+", start="+start+", length=" + length);             
                    string paddingStart = new string('�@', start);
                    string adjustedPadding = paddingStart.Replace("�@", "<size=90%>�@</size>");
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

    // �e�L�X�g���\���ɂ��郁�\�b�h
    public void HideText()
    {
        textObject.text = "";  // �e�L�X�g���\��
    }

    // �ǉ����ꂽ���\�b�h: ���̕��߂����邩���`�F�b�N����
    public bool HasNextMessage()
    {
        return currentSegmentIndex < messageSegments.Length - 1;
    }
}

