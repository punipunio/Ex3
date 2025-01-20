using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.InputSystem;
using System.Collections; // �����ǉ�


public class PracticeController : MonoBehaviour
{
    public InitialShape initialShape; // �ǉ��FInitialShape �̎Q��
    public TextManagerMincho textManagerMincho;
    public BackgroundShape backgroundShape;
    public BodyShape bodyShape;
    public TextAnswerController textAnswerController;
    public EmotionAnswerController emotionAnswerController;
    public ColliderToggle colliderToggle;

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
    private bool backgroundCleared = false;

    private string correction; 

    // UI�v�f
    public TextMeshPro resultTextField; // ���ʕ\���p��UI�e�L�X�g
    public BodyShape bodyShape_ans;
    private static int repeatCount = 0;

    private void Start()
    {   
        // �e�v�f�̏�����
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

        // ���ʕ\���p�e�L�X�g��������
        if (resultTextField != null)
        {
            resultTextField.text = ""; // �ŏ��͋�ɂ��Ă���
        }
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
                        colliderToggle.ToggleCollider(false);
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
                    colliderToggle.ToggleCollider(false);
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
                // �u�����v�܂��́u�s�����v�ɕϊ�
                string textResult = isTextCorrect ? "�Z" : "�~";
                string emotionResult = isEmotionCorrect ? "�Z" : "�~";

                string resultMessage = $"���� : {textResult}, �\�� : {emotionResult}";
                UnityEngine.Debug.Log(resultMessage);

                // 3�b�Ԍ��ʂ�\��
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
        SceneManager.LoadScene(2); // �V�[��2�ɑJ��
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
