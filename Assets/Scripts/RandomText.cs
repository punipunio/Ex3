using UnityEngine;

public class RandomText : MonoBehaviour
{
    // ���O�Ɛڑ����̔z���p��
    private string[] names = { "������", "��������", "���傤��" };
    private string[] connectors = { "��", "��", "�ɉ����" };

    // ���O�Ɛڑ����̎g�p�t���O�z���p��
    private bool[] usedNames = { false, false, false };
    private bool[] usedConnectors = { false, false, false };

    // Start is called before the first frame update
    public string GenerateText()
    {
        // �����_���ȕ��𐶐����ĕ\��
        string result = GenerateRandomSentence();
        Debug.Log(result);  // Unity�̃R���\�[���ɏo��
        return result;
    }

    // �����_���ȕ��𐶐�����֐�
    private string GenerateRandomSentence()
    {
        string result = "";

        for (int i = 0; i < 3; i++)
        {
            int nameIdx;
            int connectorIdx;

            // �g�p����Ă��Ȃ����O�������_���ɑI��
            do
            {
                nameIdx = UnityEngine.Random.Range(0, 3); // UnityEngine.Random ���g�p
            } while (usedNames[nameIdx]); // �g�p�ς݂̖��O���I�΂ꂽ�ꍇ�͍đI��
            usedNames[nameIdx] = true;  // �I���ς݂Ƀ}�[�N

            // �g�p����Ă��Ȃ��ڑ����������_���ɑI��
            do
            {
                connectorIdx = UnityEngine.Random.Range(0, 3); // UnityEngine.Random ���g�p
            } while (usedConnectors[connectorIdx]); // �g�p�ς݂̐ڑ������I�΂ꂽ�ꍇ�͍đI��
            usedConnectors[connectorIdx] = true;  // �I���ς݂Ƀ}�[�N

            // �I�΂ꂽ���O�Ɛڑ��������ʂɒǉ��i���߂���؂邽�߁u/�v���g���j
            if (i != 2) result += names[nameIdx] + connectors[connectorIdx] + "/";
            else result += names[nameIdx] + connectors[connectorIdx] + "�B" + "/";
        }

        return result; 
    }
}
