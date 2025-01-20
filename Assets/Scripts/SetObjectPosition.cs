using UnityEngine;
using UnityEngine.InputSystem;

public class SetObjectPosition : MonoBehaviour
{
    [Header("オブジェクトの初期位置と回転")]
    public Vector3 initialPosition = new Vector3(-45f, 0f, -48f);
    public Vector3 initialRotation = Vector3.zero;

    [Header("リセット用キー")]
    public Key resetKey = Key.Enter;

    void Start()
    {
        // 初期位置と回転を設定
        ResetTransform();
    }

    void Update()
    {
        // 指定されたキーが押された場合
        if (Keyboard.current != null && Keyboard.current[resetKey].wasPressedThisFrame)
        {
            ResetTransform();
            Debug.Log($"現在の位置: {transform.position}");
            Debug.Log($"現在の回転: {transform.rotation.eulerAngles}");
        }
    }

    private void ResetTransform()
    {
        // 位置と回転を設定
        transform.position = initialPosition;
        transform.rotation = Quaternion.Euler(initialRotation);

        // デバッグログで確認
        Debug.Log($"位置と回転をリセットしました:\n位置: {transform.position}\n回転: {transform.rotation.eulerAngles}");
    }
}
