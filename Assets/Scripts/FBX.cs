using UnityEngine;

public class FBXModelController : MonoBehaviour
{
  
    /// FBX���f����\������
    public void Show()
    {
        gameObject.SetActive(true); // FBX���f����\��
    }
    
    /// FBX���f�����\���ɂ���
    public void Hide()
    {
        gameObject.SetActive(false); // FBX���f�����\��
    }   
}
