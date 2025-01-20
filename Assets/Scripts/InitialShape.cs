using UnityEngine;

public class InitialShape : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float gazeTimer = 0f;
    private const float gazeDuration = 1.0f; // �������ێ����鎞�� (�b)

    private void Awake()
    {
        // SpriteRenderer �R���|�[�l���g�̎擾
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Show()
    {
        gameObject.SetActive(true); // �����}�`��\��
        SetOpacity(1.0f); // �����ǐՂ��J�n����O�ɕs�����x���ő�ɐݒ�
    }

    public void Hide()
    {
        gameObject.SetActive(false); // �����}�`���\��
    }

    public void StartGaze()
    {
        if (spriteRenderer != null)
        {
            // �������ێ����Ă���ꍇ�A���X�ɓ����ɂ���
            gazeTimer += Time.deltaTime;
            float opacity = 1 - Mathf.Clamp01(gazeTimer / gazeDuration); // �s�����x�̌v�Z
            SetOpacity(opacity);
        }
    }

    public void StopGaze()
    {
        if (spriteRenderer != null)
        {
            // �������O�ꂽ�ꍇ�A�s�����x�����Z�b�g
            gazeTimer = 0f;
            SetOpacity(1.0f);
        }
    }

    private void SetOpacity(float opacity)
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = opacity; // �s�����x��ݒ�
            spriteRenderer.color = color;
        }
    }
}

