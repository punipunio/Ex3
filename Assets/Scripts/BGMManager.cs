using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // シーンが切り替わっても破棄しない
        }
        else
        {
            Destroy(gameObject); // 重複を防ぐため既存のBGMManagerがある場合は削除
        }
    }
}
