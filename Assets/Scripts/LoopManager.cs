using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance;
    public int loopCount = 0; // 現在のループ回数
    public int totalLoops = 3; // 最大ループ回数

    private void Awake()
    {
        // シングルトンインスタンスの管理
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン間で保持
        }
        else if (Instance != this)
        {
            // 既存のインスタンスがある場合、このインスタンスを破棄
            Destroy(gameObject);
        }
    }

    public void ResetLoopManager()
    {
        totalLoops = 3;
    }
}
