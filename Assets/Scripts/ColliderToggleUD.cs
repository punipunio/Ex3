using UnityEngine;

public class ColliderToggleUD : MonoBehaviour
{
    private Collider objectCollider;
    public TextManager textManager; // TextManagerMincho の参照を取得
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
        if (textManager != null)
        {
            // originalText の "/" を取り除いた長さを取得
            string sanitizedText = textManager.originalText.Replace("/", "");
            int textLength = sanitizedText.Length;

            // シーンに配置された y, z の座標を保持
            float currentY = colliderTransform.position.y;
            float currentZ = colliderTransform.position.z;

            // originalText の長さに応じてコライダーの x 座標を変更
            float newX = colliderTransform.position.x; // デフォルトは現在の x 座標を保持

            if (textLength == 7)
            {
                newX = -45.0012f;
            }
            else if (textLength == 8)
            {
                newX = -44.9664f;
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
