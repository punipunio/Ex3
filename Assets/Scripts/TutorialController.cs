using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{

    private int buttonPressCount = 0;

    private void Update()
    {
        if (InputManagerLR.PrimaryButtonR_OnPress() ||
            InputManagerLR.PrimaryButtonL_OnPress() ||
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            buttonPressCount++; // ボタンが押された回数をカウント

            if (buttonPressCount >= 5) // 5回押された場合
            {
                LoadFirstScene(); // シーンをロード
                buttonPressCount = 0; // カウントをリセット
            }
        }
    }

    private void LoadFirstScene()
    {
        // チュートリアル終了後、MainControllerで管理する最初のシーンへ遷移
        SceneManager.LoadScene(1); // シーン1に遷移 (ビルド設定でインデックス1を最初のシーンに指定)
    }
}
