using UnityEngine;

public class ColliderToggle : MonoBehaviour
{
    private Collider objectCollider;
    public TextManagerMincho textManagerMincho; // TextManagerMincho の参照を取得
    private Transform colliderTransform;

    private void Start()
    {
        // このスクリプトがアタッチされているオブジェクトのコライダーを取得
        objectCollider = GetComponent<Collider>();
        colliderTransform = objectCollider.transform;

        // originalText の長さに応じてコライダーの位置を設定
        AdjustColliderPositionBasedOnTextLength();     
    }  

    private void AdjustColliderPositionBasedOnTextLength()
    {
        if (textManagerMincho != null)
        {
            // originalText の "/" を取り除いた長さを取得
            string sanitizedText = textManagerMincho.originalText.Replace("/", "");
            int textLength = sanitizedText.Length;

            // シーンで設定されている y, z 座標を保持
            float currentY = colliderTransform.position.y;
            float currentZ = colliderTransform.position.z;

            // originalText の長さに応じてコライダーの x 座標を変更
            float newX = colliderTransform.position.x; // デフォルトは現在の x 座標を保持

            if (textLength == 7)
            {
                newX = -44.9921f;
            }
            else if (textLength == 8)
            {
                newX = -44.9478f;
            }

            // x のみ変更し、y と z はそのままの値を使用
            colliderTransform.position = new Vector3(newX, currentY, currentZ);
        }
        else
        {
            Debug.LogError("TextManagerMincho is not assigned in ColliderToggle.");
        }
    }

    public void ToggleCollider(bool isEnabled)
    {
        if (objectCollider != null)
        {
            objectCollider.enabled = isEnabled; // ON/OFF 切り替え
        }
    }
}
