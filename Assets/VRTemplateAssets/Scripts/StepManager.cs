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
using UnityEngine.SceneManagement; // シーン管理の名前空間

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
        private string nextSceneName; // 次のシーン名を指定

        int m_CurrentStepIndex = 0;

        public void Next()
        {
            // 現在のステップを非表示
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);

            // 次のステップへ進む
            m_CurrentStepIndex++;

            // 最後のステップかどうかをチェック
            if (m_CurrentStepIndex >= m_StepList.Count)
            {
                // 次のシーンをロード
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.LogError("次のシーン名が指定されていません！");
                }
            }
            else
            {
                // 次のステップを表示
                m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
                m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].buttonText;
            }
        }
    }
}
