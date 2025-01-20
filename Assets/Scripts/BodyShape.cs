using UnityEngine;

public class BodyShape : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true); // 初期図形を表示
    }

    public void Hide()
    {
        gameObject.SetActive(false); // 初期図形を非表示
    }
}
