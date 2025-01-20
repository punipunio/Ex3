using UnityEngine;

public class FBXModelController : MonoBehaviour
{
  
    /// FBXモデルを表示する
    public void Show()
    {
        gameObject.SetActive(true); // FBXモデルを表示
    }
    
    /// FBXモデルを非表示にする
    public void Hide()
    {
        gameObject.SetActive(false); // FBXモデルを非表示
    }   
}
