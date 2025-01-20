using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.InputSystem;
using System.Drawing;

public class MainControllerUD : MonoBehaviour
{
    public InitialShape initialShape; // 追加：InitialShape の参照
    public TextManager textManager;
    public BackgroundShape backgroundShape;
    public BodyShape bodyShape;
    public TextAnswerController textAnswerController;
    public EmotionAnswerController emotionAnswerController;
    public ColliderToggleUD colliderToggleUD;

    // 視線追跡用
    public GameObject gazeTarget; // 視線を検出する対象物
    public GameObject fullStop; //句点
    public GameObject face;
    public LayerMask targetLayer;
    private float gazeTimer = 0f;
    private const float gazeDuration = 1.0f; // 視線を維持する時間 (秒) 注視図形
    private const float gazeDuration2 = 2.0f; // 視線を維持する時間 (秒) 表情
    private bool isGazing = false;
    private bool wasGazingAtFullStop = false; // fullStop に視線が当たっていたかどうか

    // アイトラッキングコンポーネント   
    public EyeGazeController eyeGazeController;

    private Stopwatch timer1 = new Stopwatch();
    private Stopwatch timer2 = new Stopwatch();
    private long? elapsedTime2 = 0;
    private bool backgroundCleared = false;

    private string correction;

    // シーンループ管理
    private int sceneStartIndex = 3; // シーン1のインデックス
    private int sceneEndIndex = 17; // シーンnのインデックス

    private void Start()
    {
        // 各要素の初期化
        colliderToggleUD.ToggleCollider(false);
        backgroundCleared = false;
        initialShape.Show();
        textManager.HideText();
        backgroundShape.SetRandomBackground();
        backgroundShape.Hide();
        bodyShape.Show();
        textAnswerController.HideButtons();
        emotionAnswerController.HideButtons();
    }

    private void Update()
    {
        // 視線の方向からレイを作成
        Ray gazeRay = new Ray(eyeGazeController.eyeGaze.transform.position, eyeGazeController.eyeGaze.transform.forward);

        // 視線がターゲットを検出しているかチェック
        if (Physics.Raycast(gazeRay, out RaycastHit hit, Mathf.Infinity, targetLayer))
        {
            //UnityEngine.Debug.Log($"Ray hit detected: {hit.collider.gameObject.name}");
            // fullStop に視線が当たった場合
            if (hit.collider.gameObject == fullStop)
            {
                if (!wasGazingAtFullStop)
                {
                    // 初めて fullStop に視線が当たったタイミングの処理
                    wasGazingAtFullStop = true;
                }
            }
            else
            {
                // fullStop に視線が当たっていた後、他のオブジェクトに移った場合
                if (wasGazingAtFullStop)
                {
                    // fullStop から視線が外れたときの処理
                    wasGazingAtFullStop = false;

                    if (!timer2.IsRunning && !backgroundCleared)
                    {
                        timer1.Stop();
                        timer2.Start();
                        //textManagerMincho.HideText();
                        colliderToggleUD.ToggleCollider(false);
                    }
                }
            }

            if (hit.collider.gameObject == gazeTarget)
            {
                //UnityEngine.Debug.Log("Gaze target detected.");
                isGazing = true;
                gazeTimer += Time.deltaTime;

                // 視線を維持している場合は InitialShape の不透明度を変更
                initialShape.StartGaze();

                // 1秒間視線を維持したら、次のフェーズへ進む
                if (gazeTimer >= gazeDuration && CanStartPhase())
                {
                    timer1.Start();
                    initialShape.Hide();
                    backgroundShape.SetRandomBackground();
                    backgroundShape.Show();
                    colliderToggleUD.ToggleCollider(true);
                    textManager.ShowNextSegment();
                }
            }

            else if (hit.collider.gameObject == face)
            {
                if (timer1.IsRunning)
                {
                    timer1.Stop();
                    elapsedTime2 = null;
                }
                timer2.Stop();
                isGazing = true;
                gazeTimer += Time.deltaTime;

                // 2秒間視線を維持したら、次のフェーズへ進む
                if (gazeTimer >= gazeDuration2 && (!backgroundCleared))
                {
                    bodyShape.Hide();
                    backgroundShape.Hide();
                    backgroundCleared = true;
                    textAnswerController.ShowButtons();
                    ResetGaze();
                    UnityEngine.Debug.Log("timer1 : " + timer1.ElapsedMilliseconds + ", timer2 : " + timer2.ElapsedMilliseconds);
                    StartAnswerPhase();
                }
            }
            else
            {
                //UnityEngine.Debug.Log("Gaze target lost. Resetting gaze.");
                ResetGaze();
                initialShape.StopGaze();
            }
        }
        else
        {
            ResetGaze();
            initialShape.StopGaze();
            // fullStop に視線が当たっていた後、何も検出されなくなった場合
            if (wasGazingAtFullStop)
            {
                // fullStop から視線が外れたときの処理
                wasGazingAtFullStop = false;

                if (!timer2.IsRunning && !backgroundCleared)
                {
                    timer1.Stop();
                    timer2.Start();
                    //textManagerMincho.HideText();
                    colliderToggleUD.ToggleCollider(false);
                }
            }
        }

        // ボタン入力の処理（従来の方法も残す場合）
        //InputManagerLR.PrimaryButtonR_OnPress() ||InputManagerLR.PrimaryButtonL_OnPress() ||InputManagerLR.SecondaryButtonR_OnPress() ||
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            HandleSpaceKeyPress();
        }
    }

    private void HandleSpaceKeyPress()
    {
        if (CanStartPhase())
        {
            timer1.Start();
            initialShape.Hide();
            backgroundShape.Show();
            colliderToggleUD.ToggleCollider(true);
            textManager.ShowNextSegment();
        }
        else if (textManager.HasNextMessage())
        {
            textManager.ShowNextSegment();
        }
        else if (!timer2.IsRunning && !backgroundCleared)
        {
            timer1.Stop();
            timer2.Start();
            //textManagerMincho.HideText();
            colliderToggleUD.ToggleCollider(false);
        }
        else if (!backgroundCleared && timer2.IsRunning)
        {
            timer2.Stop();
            bodyShape.Hide();
            backgroundShape.Hide();
            backgroundCleared = true;

            textAnswerController.ShowButtons();
            StartAnswerPhase();
        }
    }

    private void StartAnswerPhase()
    {
        textAnswerController.StartTextAnswerPhase((isTextCorrect) =>
        {
            emotionAnswerController.StartEmotionAnswerPhase((isEmotionCorrect) =>
            {
                UnityEngine.Debug.Log($"Text Correct: {isTextCorrect}, Emotion Correct: {isEmotionCorrect}");
                if (isTextCorrect && isEmotionCorrect)
                {
                    correction = "T";
                    LogDataForCurrentScene();
                    LoadNextScene(); // 次のシーンへ
                }
                else
                {
                    correction = "F";
                    LogDataForCurrentScene();
                    ReloadCurrentScene(); // 不正解の場合は同じシーンを再読み込み
                }
            });
        });
    }

    //Meta quest proでビルドしたとき用
    //private void LogDataForCurrentScene()
    //{
    //    LoopManager.Instance.ResetLoopManager();
    //    string sceneName = SceneManager.GetActiveScene().name;
    //    string generatedText = textManagerMincho.originalText;
    //    string backgroundName = backgroundShape.GetSelectedBackgroundName();

    //    string sanitizedText = generatedText.Replace(",", "，");
    //    string row = $"{sceneName},{timer1.ElapsedMilliseconds},{timer2.ElapsedMilliseconds},{timer1.ElapsedMilliseconds + timer2.ElapsedMilliseconds},{backgroundName},\"{sanitizedText}\",{correction}";

    //    DataManager.Instance.AddCsvRow(row);

    //    // 最終シーンの場合にCSVを保存
    //    if (SceneManager.GetActiveScene().buildIndex == sceneEndIndex && LoopManager.Instance.loopCount == LoopManager.Instance.totalLoops - 1)
    //    {
    //        // ファイル保存パスを指定 (Meta Quest Pro の内部ストレージに保存)
    //        string filePath = Application.persistentDataPath + "/output.csv";
    //        UnityEngine.Debug.Log($"CSV will be saved to: {filePath}");
    //        DataManager.Instance.SaveCsv(filePath);

    //        LoadScene(); // 全ループ終了時にゲームを終了
    //    }
    //}

    private void LogDataForCurrentScene() //PC実行用
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string generatedText = textManager.originalText;
        string backgroundName = backgroundShape.GetSelectedBackgroundName();
        if (elapsedTime2 != null)
        {
            elapsedTime2 = timer2.ElapsedMilliseconds;
        }

        string sanitizedText = generatedText.Replace(",", "，");
        string row = $"{sceneName},{timer1.ElapsedMilliseconds},{elapsedTime2},{timer1.ElapsedMilliseconds + timer2.ElapsedMilliseconds},{backgroundName},\"{sanitizedText}\",{correction}";

        DataManager.Instance.AddCsvRow(row);

        // 最終シーンの場合にCSVを保存
        if (SceneManager.GetActiveScene().buildIndex == sceneEndIndex && LoopManager.Instance.loopCount == LoopManager.Instance.totalLoops - 1)
        {
            string filePath = Application.persistentDataPath + "/output.csv";
            DataManager.Instance.SaveCsv(filePath);

            LoadScene(); // 全ループ終了時にゲームを終了
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex >= sceneStartIndex && currentSceneIndex < sceneEndIndex)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else if (currentSceneIndex == sceneEndIndex)
        {
            // LoopManagerを使用してループ回数を増やす
            LoopManager.Instance.loopCount++;

            if (LoopManager.Instance.loopCount < LoopManager.Instance.totalLoops)
            {
                LoadScene();
            }
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneEndIndex + 1);
    }

    private void ReloadCurrentScene()
    {
        // 現在のシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    private bool CanStartPhase()
    {
        return !timer1.IsRunning && !timer2.IsRunning && !backgroundCleared;
    }


    private void ResetGaze()
    {
        if (isGazing)
        {
            //UnityEngine.Debug.Log("Resetting gaze timer.");
        }
        isGazing = false;
        gazeTimer = 0f;
    }
}
