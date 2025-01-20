using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGazeController : MonoBehaviour
{
    public GameObject pointer; // ポインターオブジェクト
    public OVREyeGaze eyeGaze; // OVREyeGaze コンポーネント
    public float maxGazeDistance = 10.0f; // 視線の最大距離
    public float smoothingSpeed = 10.0f; // スムージングの速度
    public Vector3 gazeOffset = new Vector3(0, 0, 0); // 視線方向の補正値

    void Start()
    {
        eyeGaze = GetComponent<OVREyeGaze>();   
    }

    void Update()
    {
        if (eyeGaze != null && eyeGaze.EyeTrackingEnabled)
        {
            Vector3 gazeOrigin = eyeGaze.transform.position; // 視線の起点
            Vector3 gazeDirection = eyeGaze.transform.forward + gazeOffset; // 視線の方向（補正値を加える）

            // レイキャストで視線がヒットした位置を取得
            RaycastHit hit;
            Vector3 targetPosition;
            if (Physics.Raycast(gazeOrigin, gazeDirection, out hit, maxGazeDistance))
            {
                targetPosition = hit.point;
                pointer.transform.forward = hit.normal; // ポインターの向き調整
            }
            else
            {
                // ヒットしない場合のデフォルト位置
                targetPosition = gazeOrigin + gazeDirection * maxGazeDistance;
                pointer.transform.forward = gazeDirection;
            }

            // スムージングを適用してポインターを移動
            pointer.transform.position = Vector3.Lerp(pointer.transform.position, targetPosition, Time.deltaTime * smoothingSpeed);
        }
    }
}


