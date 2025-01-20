using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSetup : MonoBehaviour
{
    public Camera mainCamera; // メインカメラを指定

    void Start()
    {
        // メインカメラを自動取得（未設定の場合）
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }     

        // カメラの向きをZ軸正方向に設定
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0); // 回転角度を指定（0, 0, 0はZ軸正方向）
    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame) 
        {
            // カメラの向きをZ軸正方向に設定
            mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0); // 回転角度を指定（0, 0, 0はZ軸正方向）
        }
    }
}
