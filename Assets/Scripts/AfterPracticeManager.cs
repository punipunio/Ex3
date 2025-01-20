using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterPracticeManager : MonoBehaviour
{
    [SerializeField]
    private float timeToEndScene = 5f; // シーン終了までの時間（秒）

    private float timer = 0f; // 経過時間を追跡

    void Update()
    {
        timer += Time.deltaTime; // 時間を加算

        if (timer >= timeToEndScene) // 7秒経過したら
        {
            LoadNextScene(); // シーン終了処理
        }
    }

    private void LoadNextScene()
    {
        // チュートリアル終了後、MainControllerで管理する最初のシーンへ遷移
        SceneManager.LoadScene(3); // シーン1に遷移 (ビルド設定でインデックス1を最初のシーンに指定)
    }
}
