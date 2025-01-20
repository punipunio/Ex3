using UnityEngine;

public class MoveChildAbsolute : MonoBehaviour
{
    public Transform childObject;  // 子オブジェクトのTransform
    public Vector3 newWorldPosition;  // 移動先のワールド座標

    void Update()
    {
        if (childObject != null)
        {
            // 子オブジェクトを絶対座標に移動
            childObject.position = newWorldPosition;
        }
    }
}
