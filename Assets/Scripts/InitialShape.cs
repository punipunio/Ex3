using UnityEngine;

public class InitialShape : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float gazeTimer = 0f;
    private const float gazeDuration = 1.0f; // 視線を維持する時間 (秒)

    private void Awake()
    {
        // SpriteRenderer コンポーネントの取得
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Show()
    {
        gameObject.SetActive(true); // 初期図形を表示
        SetOpacity(1.0f); // 視線追跡を開始する前に不透明度を最大に設定
    }

    public void Hide()
    {
        gameObject.SetActive(false); // 初期図形を非表示
    }

    public void StartGaze()
    {
        if (spriteRenderer != null)
        {
            // 視線を維持している場合、徐々に透明にする
            gazeTimer += Time.deltaTime;
            float opacity = 1 - Mathf.Clamp01(gazeTimer / gazeDuration); // 不透明度の計算
            SetOpacity(opacity);
        }
    }

    public void StopGaze()
    {
        if (spriteRenderer != null)
        {
            // 視線が外れた場合、不透明度をリセット
            gazeTimer = 0f;
            SetOpacity(1.0f);
        }
    }

    private void SetOpacity(float opacity)
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = opacity; // 不透明度を設定
            spriteRenderer.color = color;
        }
    }
}

