using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class EyeGazePointer : MonoBehaviour
{
    public GameObject pointerObject; // ポインターとして使うオブジェクト
    public float maxDistance = 5.0f; // 視線を追従する最大距離
    public LayerMask collisionLayer; // 衝突を検出するレイヤーマスク

    private InputDevice eyeGazeDevice;
    private InputControl poseControl;


    void Start()
    {
        // Eye Gaze デバイスを検索
        foreach (var device in InputSystem.devices)
        {
            if (device.layout == "EyeGaze")
            {
                eyeGazeDevice = device;
                poseControl = eyeGazeDevice["pose"];
                Debug.Log("Eye Gaze device found.");
                break;
            }
        }

        if (eyeGazeDevice == null)
        {
            Debug.LogError("No Eye Gaze device found.");
        }
    }

    void Update()
    {
        if (eyeGazeDevice != null && poseControl != null && poseControl is InputControl<PoseState> poseStateControl)
        {
            PoseState poseState = poseStateControl.ReadValue();

            if (poseState.isTracked)
            {
                Vector3 gazeOrigin = poseState.position;
                Vector3 gazeDirection = poseState.rotation * Vector3.forward;

                // 衝突を検出
                if (Physics.Raycast(gazeOrigin, gazeDirection, out RaycastHit hit, maxDistance, collisionLayer))
                {
                    // 視線が衝突した位置にポインターを移動
                    pointerObject.transform.position = hit.point;
                }
                else
                {
                    // 衝突しない場合は視線の方向にポインターを移動
                    pointerObject.transform.position = gazeOrigin + gazeDirection * maxDistance;
                }

                // ポインターをアクティブ化
                if (!pointerObject.activeSelf)
                {
                    pointerObject.SetActive(true);
                }
            }
            if (poseState.isTracked)
            {
                Debug.Log($"Gaze Origin: {poseState.position}, Gaze Direction: {poseState.rotation * Vector3.forward}");
            }
            else
            {
                Debug.Log("Eye gaze is not being tracked.");
            }

        }
        else
        {
            // デバイスが無効な場合、ポインターを非表示にする
            if (pointerObject.activeSelf)
            {
                pointerObject.SetActive(false);
            }
        }
    }
}

