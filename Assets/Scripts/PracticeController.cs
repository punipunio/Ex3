using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.InputSystem;
using System.Collections; // これを追加


public class PracticeController : MonoBehaviour
{
    public InitialShape initialShape; // 追加：InitialShape の参照
    public TextManagerMincho textManagerMincho;
    public BackgroundShape backgroundShape;
    public BodyShape bodyShape;
    public TextAnswerController textAnswerController;
    public EmotionAnswerController emotionAnswerController;
    public ColliderToggle colliderToggle;

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
    private bool backgroundCleared = false;

    private string correction; 

    // UI要素
    public TextMeshPro resultTextField; // 結果表示用のUIテキスト
    public BodyShape bodyShape_ans;
    private static int repeatCount = 0;

    private void Start()
    {   
        // 各要素の初期化
        colliderToggle.ToggleCollider(false);
        backgroundCleared = false;
        initialShape.Show();
        textManagerMincho.HideText();
        backgroundShape.SetRandomBackground();
        backgroundShape.Hide();
        bodyShape.Show();
        bodyShape_ans.Hide();
        textAnswerController.HideButtons();
        emotionAnswerController.HideButtons();

        // 結果表示用テキストを初期化
        if (resultTextField != null)
        {
            resultTextField.text = ""; // 最初は空にしておく
        }
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
                        colliderToggle.ToggleCollider(false);
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
                    backgroundShape.Show();
                    colliderToggle.ToggleCollider(true);
                    textManagerMincho.ShowNextSegment();
                }
            }

            else if (hit.collider.gameObject == face)
            {
                if (timer1.IsRunning)
                {
                    timer1.Stop();
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
                    colliderToggle.ToggleCollider(false);
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
            backgroundShape.SetRandomBackground();
            backgroundShape.Show();
            colliderToggle.ToggleCollider(true);
            textManagerMincho.ShowNextSegment();
        }
        else if (textManagerMincho.HasNextMessage())
        {
            textManagerMincho.ShowNextSegment();
        }
        else if (!timer2.IsRunning && !backgroundCleared)
        {
            timer1.Stop();
            timer2.Start();
            textManagerMincho.HideText();
            colliderToggle.ToggleCollider(false);
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
                // 「正解」または「不正解」に変換
                string textResult = isTextCorrect ? "〇" : "×";
                string emotionResult = isEmotionCorrect ? "〇" : "×";

                string resultMessage = $"文章 : {textResult}, 表情 : {emotionResult}";
                UnityEngine.Debug.Log(resultMessage);

                // 3秒間結果を表示
                StartCoroutine(ShowResultAndProceed(resultMessage, isTextCorrect, isEmotionCorrect));
            });
        });
    }


    private IEnumerator ShowResultAndProceed(string resultMessage, bool isTextCorrect, bool isEmotionCorrect)
    {
        if (resultTextField != null)
        {
            bodyShape_ans.Show();
            resultTextField.text = resultMessage;
        }

        yield return new WaitForSeconds(3f);

        if (resultTextField != null)
        {
            resultTextField.text = "";
        }

        correction = isTextCorrect && isEmotionCorrect ? "T" : "F";
        repeatCount++;

        if (repeatCount >= 5)
        {
            LoadNextScene();
        }
        else
        {
            ReloadCurrentScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(2); // シーン2に遷移
    }

    private void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private bool CanStartPhase()
    {
        return !timer1.IsRunning && !timer2.IsRunning && !backgroundCleared;
    }

    private void ResetGaze()
    {
        isGazing = false;
        gazeTimer = 0f;
    }
}
