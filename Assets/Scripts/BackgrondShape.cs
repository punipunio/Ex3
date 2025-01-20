using UnityEngine;
using UnityEngine.Rendering;

public class BackgroundShape : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // 背景画像を表示するSpriteRenderer
    public Sprite[] backgroundSprites;   // 複数の背景画像
    private string selectedSpriteName;   // 現在選択されているスプライト名
    private string[] emotions = {"真顔", "笑顔", "悲しみ"};
    private int randomIndex;
    private Collider objectCollider;     // コライダー参照

    private void Awake()
    {
        // ゲームオブジェクトにアタッチされているコライダーを取得
        objectCollider = GetComponent<Collider>();    
    }

    public void Show()
    {
        gameObject.SetActive(true); // 背景図形を表示
        objectCollider.enabled = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false); // 背景図形を非表示
        objectCollider.enabled = false;
    }

    // ランダムに背景画像を設定
    public void SetRandomBackground()
    {
        if (backgroundSprites.Length == 0)
        {
            Debug.LogError("背景画像が設定されていません！");
            return;
        }

        // 確率分布に基づいてrandomIndexを決定
        randomIndex = Random.Range(0,6);
        spriteRenderer.sprite = backgroundSprites[randomIndex];
        selectedSpriteName = backgroundSprites[randomIndex].name; // 選択されたスプライト名を保存
        Debug.Log(selectedSpriteName);
        return;
    }

    public string GetSelectedBackgroundName()
    {
        return selectedSpriteName; // 現在選択されているスプライト名を返す
    }

    public int CorrectAnswer()
    {
        return (randomIndex) % 3;
    }

    // 確率分布に基づいてインデックスを選択する関数
    private int GetWeightedRandomIndex()
    {
        // 1,3,7を20%ずつ、0,2,4,5,6を8%ずつに設定
        float[] probabilities = { 0.08f, 0.20f, 0.08f, 0.20f, 0.08f, 0.08f, 0.08f, 0.20f };
        float randomValue = Random.value; // 0.0〜1.0のランダムな値を取得

        float cumulativeProbability = 0f;
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                return i; // 確率に応じたインデックスを返す
            }
        }

        // 万が一全ての条件を満たさない場合
        return probabilities.Length - 1;
    }
}

