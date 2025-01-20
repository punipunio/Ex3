using UnityEngine;
using UnityEngine.InputSystem;

public class ResetXRPositionAndRotation : MonoBehaviour
{
    public Transform xrOrigin;  // XR OriginのTransform
    public Transform mainCamera; // Main CameraのTransform

    public Transform targetObject;  // xrOriginを移動させるターゲットオブジェクト

    public Vector3 targetDirection = new Vector3(0f, 0f, -1f);  // リセット後に向かせたい方向（設定可能）

    private Vector3 initialPosition; // 初期位置を保持する変数

    void Start()
    {
        // 初期位置を設定（位置のリセットは一度だけ行う）
        if (xrOrigin != null)
        {
            initialPosition = xrOrigin.position;  // 初期位置を保持
        }
    }

    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame || InputManagerLR.SecondaryButtonL_OnPress())
        {
            // 'Enter'キーまたは指定したボタンが押されたときに向きをリセットする
            ResetRotation();
        }
    }

    // 現在のカメラの向きとリセット後の方向の角度差を計算し、回転を適用
    void ResetRotation()
    {
        if (xrOrigin != null && mainCamera != null)
        {
            // XR Originをターゲットオブジェクトの位置に移動
            if (targetObject != null)
            {
                xrOrigin.position = targetObject.position;  // ターゲットオブジェクトの位置に移動
            }

            // 現在のカメラの向き（カメラのforward方向）
            Vector3 currentDirection = mainCamera.forward;
            currentDirection.y = 0f;  // 水平方向の向きだけを使用

            // 向かせたい方向（設定されたtargetDirection）
            Vector3 desiredDirection = targetDirection.normalized;

            // 現在の方向と指定した方向の間の角度を計算
            float angle = Vector3.SignedAngle(currentDirection, desiredDirection, Vector3.up);

            // 回転を適用（角度を指定方向に回転させる）
            xrOrigin.Rotate(Vector3.up, angle, Space.World);  // 水平軸（y軸）周りに回転させる
        }
    }
}
