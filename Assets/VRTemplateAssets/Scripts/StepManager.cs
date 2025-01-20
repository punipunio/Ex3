//using System;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//namespace Unity.VRTemplate
//{
//    /// <summary>
//    /// Controls the steps in the in coaching card.
//    /// </summary>
//    public class StepManager : MonoBehaviour
//    {
//        [Serializable]
//        class Step
//        {
//            [SerializeField]
//            public GameObject stepObject;

//            [SerializeField]
//            public string buttonText;
//        }

//        [SerializeField]
//        public TextMeshProUGUI m_StepButtonTextField;

//        [SerializeField]
//        List<Step> m_StepList = new List<Step>();

//        int m_CurrentStepIndex = 0;

//        public void Next()
//        {
//            m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);
//            m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;
//            m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
//            m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].buttonText;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���Ǘ��̖��O���

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controls the steps in the in coaching card.
    /// </summary>
    public class StepManager : MonoBehaviour
    {
        [Serializable]
        class Step
        {
            [SerializeField]
            public GameObject stepObject;

            [SerializeField]
            public string buttonText;
        }

        [SerializeField]
        public TextMeshProUGUI m_StepButtonTextField;

        [SerializeField]
        List<Step> m_StepList = new List<Step>();

        [SerializeField]
        private string nextSceneName; // ���̃V�[�������w��

        int m_CurrentStepIndex = 0;

        public void Next()
        {
            // ���݂̃X�e�b�v���\��
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);

            // ���̃X�e�b�v�֐i��
            m_CurrentStepIndex++;

            // �Ō�̃X�e�b�v���ǂ������`�F�b�N
            if (m_CurrentStepIndex >= m_StepList.Count)
            {
                // ���̃V�[�������[�h
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.LogError("���̃V�[�������w�肳��Ă��܂���I");
                }
            }
            else
            {
                // ���̃X�e�b�v��\��
                m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
                m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].buttonText;
            }
        }
    }
}
