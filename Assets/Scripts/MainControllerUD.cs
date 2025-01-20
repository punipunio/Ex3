using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.InputSystem;
using System.Drawing;

public class MainControllerUD : MonoBehaviour
{
    public InitialShape initialShape; // �ǉ��FInitialShape �̎Q��
    public TextManager textManager;
    public BackgroundShape backgroundShape;
    public BodyShape bodyShape;
    public TextAnswerController textAnswerController;
    public EmotionAnswerController emotionAnswerController;
    public ColliderToggleUD colliderToggleUD;

    // �����ǐ՗p
    public GameObject gazeTarget; // ���������o����Ώە�
    public GameObject fullStop; //��_
    public GameObject face;
    public LayerMask targetLayer;
    private float gazeTimer = 0f;
    private const float gazeDuration = 1.0f; // �������ێ����鎞�� (�b) �����}�`
    private const float gazeDuration2 = 2.0f; // �������ێ����鎞�� (�b) �\��
    private bool isGazing = false;
    private bool wasGazingAtFullStop = false; // fullStop �Ɏ������������Ă������ǂ���

    // �A�C�g���b�L���O�R���|�[�l���g   
    public EyeGazeController eyeGazeController;

    private Stopwatch timer1 = new Stopwatch();
    private Stopwatch timer2 = new Stopwatch();
    private long? elapsedTime2 = 0;
    private bool backgroundCleared = false;

    private string correction;

    // �V�[�����[�v�Ǘ�
    private int sceneStartIndex = 3; // �V�[��1�̃C���f�b�N�X
    private int sceneEndIndex = 17; // �V�[��n�̃C���f�b�N�X

    private void Start()
    {
        // �e�v�f�̏�����
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
        // �����̕������烌�C���쐬
        Ray gazeRay = new Ray(eyeGazeController.eyeGaze.transform.position, eyeGazeController.eyeGaze.transform.forward);

        // �������^�[�Q�b�g�����o���Ă��邩�`�F�b�N
        if (Physics.Raycast(gazeRay, out RaycastHit hit, Mathf.Infinity, targetLayer))
        {
            //UnityEngine.Debug.Log($"Ray hit detected: {hit.collider.gameObject.name}");
            // fullStop �Ɏ��������������ꍇ
            if (hit.collider.gameObject == fullStop)
            {
                if (!wasGazingAtFullStop)
                {
                    // ���߂� fullStop �Ɏ��������������^�C�~���O�̏���
                    wasGazingAtFullStop = true;
                }
            }
            else
            {
                // fullStop �Ɏ������������Ă�����A���̃I�u�W�F�N�g�Ɉڂ����ꍇ
                if (wasGazingAtFullStop)
                {
                    // fullStop ���王�����O�ꂽ�Ƃ��̏���
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

                // �������ێ����Ă���ꍇ�� InitialShape �̕s�����x��ύX
                initialShape.StartGaze();

                // 1�b�Ԏ������ێ�������A���̃t�F�[�Y�֐i��
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

                // 2�b�Ԏ������ێ�������A���̃t�F�[�Y�֐i��
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
            // fullStop �Ɏ������������Ă�����A�������o����Ȃ��Ȃ����ꍇ
            if (wasGazingAtFullStop)
            {
                // fullStop ���王�����O�ꂽ�Ƃ��̏���
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

        // �{�^�����͂̏����i�]���̕��@���c���ꍇ�j
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
                    LoadNextScene(); // ���̃V�[����
                }
                else
                {
                    correction = "F";
                    LogDataForCurrentScene();
                    ReloadCurrentScene(); // �s�����̏ꍇ�͓����V�[�����ēǂݍ���
                }
            });
        });
    }

    //Meta quest pro�Ńr���h�����Ƃ��p
    //private void LogDataForCurrentScene()
    //{
    //    LoopManager.Instance.ResetLoopManager();
    //    string sceneName = SceneManager.GetActiveScene().name;
    //    string generatedText = textManagerMincho.originalText;
    //    string backgroundName = backgroundShape.GetSelectedBackgroundName();

    //    string sanitizedText = generatedText.Replace(",", "�C");
    //    string row = $"{sceneName},{timer1.ElapsedMilliseconds},{timer2.ElapsedMilliseconds},{timer1.ElapsedMilliseconds + timer2.ElapsedMilliseconds},{backgroundName},\"{sanitizedText}\",{correction}";

    //    DataManager.Instance.AddCsvRow(row);

    //    // �ŏI�V�[���̏ꍇ��CSV��ۑ�
    //    if (SceneManager.GetActiveScene().buildIndex == sceneEndIndex && LoopManager.Instance.loopCount == LoopManager.Instance.totalLoops - 1)
    //    {
    //        // �t�@�C���ۑ��p�X���w�� (Meta Quest Pro �̓����X�g���[�W�ɕۑ�)
    //        string filePath = Application.persistentDataPath + "/output.csv";
    //        UnityEngine.Debug.Log($"CSV will be saved to: {filePath}");
    //        DataManager.Instance.SaveCsv(filePath);

    //        LoadScene(); // �S���[�v�I�����ɃQ�[�����I��
    //    }
    //}

    private void LogDataForCurrentScene() //PC���s�p
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string generatedText = textManager.originalText;
        string backgroundName = backgroundShape.GetSelectedBackgroundName();
        if (elapsedTime2 != null)
        {
            elapsedTime2 = timer2.ElapsedMilliseconds;
        }

        string sanitizedText = generatedText.Replace(",", "�C");
        string row = $"{sceneName},{timer1.ElapsedMilliseconds},{elapsedTime2},{timer1.ElapsedMilliseconds + timer2.ElapsedMilliseconds},{backgroundName},\"{sanitizedText}\",{correction}";

        DataManager.Instance.AddCsvRow(row);

        // �ŏI�V�[���̏ꍇ��CSV��ۑ�
        if (SceneManager.GetActiveScene().buildIndex == sceneEndIndex && LoopManager.Instance.loopCount == LoopManager.Instance.totalLoops - 1)
        {
            string filePath = Application.persistentDataPath + "/output.csv";
            DataManager.Instance.SaveCsv(filePath);

            LoadScene(); // �S���[�v�I�����ɃQ�[�����I��
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
            // LoopManager���g�p���ă��[�v�񐔂𑝂₷
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
        // ���݂̃V�[�����ēǂݍ���
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
