using UnityEngine;

public class ColliderToggleUD : MonoBehaviour
{
    private Collider objectCollider;
    public TextManager textManager; // TextManagerMincho �̎Q�Ƃ��擾
    private Transform colliderTransform;

    private void Start()
    {
        // ���̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g�̃R���C�_�[���擾
        objectCollider = GetComponent<Collider>();
        colliderTransform = objectCollider.transform;

        // originalText �̒����ɉ����ăR���C�_�[�̈ʒu��ݒ�
        AdjustColliderPositionBasedOnTextLength();
    }

    private void AdjustColliderPositionBasedOnTextLength()
    {
        if (textManager != null)
        {
            // originalText �� "/" ����菜�����������擾
            string sanitizedText = textManager.originalText.Replace("/", "");
            int textLength = sanitizedText.Length;

            // �V�[���ɔz�u���ꂽ y, z �̍��W��ێ�
            float currentY = colliderTransform.position.y;
            float currentZ = colliderTransform.position.z;

            // originalText �̒����ɉ����ăR���C�_�[�� x ���W��ύX
            float newX = colliderTransform.position.x; // �f�t�H���g�͌��݂� x ���W��ێ�

            if (textLength == 7)
            {
                newX = -45.0012f;
            }
            else if (textLength == 8)
            {
                newX = -44.9664f;
            }

            // x �̂ݕύX���Ay �� z �͂��̂܂܂̒l���g�p
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
            objectCollider.enabled = isEnabled; // ON/OFF �؂�ւ�
        }
    }
}
