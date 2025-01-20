using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    public LoopManager loopManager;
    public TextMeshPro sentence;

    // シーンループ管理
    private int sceneStartIndex = 3; // シーン1のインデックス

    [SerializeField]
    private float timeToEndScene = 3f; // シーン終了までの時間（秒）

    private float timer = 0f; // 経過時間を追跡

    void Update()
    {
        timer += Time.deltaTime; // 時間を加算

        SetController();
    }

    private void QuitGame()
    {
        UnityEngine.Debug.Log("This is the last scene.");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void SetController() 
    {
        string result = sentence.text;

        if(LoopManager.Instance.loopCount < LoopManager.Instance.totalLoops) 
        {
            sentence.text = $"{LoopManager.Instance.loopCount}セット目が終了しました。\n残り{LoopManager.Instance.totalLoops - LoopManager.Instance.loopCount}セットです。";
            if (timer >= timeToEndScene)
            {
                timer = 0f;
                SceneManager.LoadScene(sceneStartIndex);
            }
        }
        else if(LoopManager.Instance.loopCount == LoopManager.Instance.totalLoops) 
        {
            sentence.text = $"実験終了です。\nご協力ありがとうございました。";
            if (timer >= timeToEndScene)
            {
                QuitGame(); // シーン終了処理
            }
        }
    }
}
