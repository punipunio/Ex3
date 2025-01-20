using UnityEngine;

public class ColliderToggle : MonoBehaviour
{
    private Collider objectCollider;
    public TextManagerMincho textManagerMincho; // TextManagerMincho �̎Q�Ƃ��擾
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
        if (textManagerMincho != null)
        {
            // originalText �� "/" ����菜�����������擾
            string sanitizedText = textManagerMincho.originalText.Replace("/", "");
            int textLength = sanitizedText.Length;

            // �V�[���Őݒ肳��Ă��� y, z ���W��ێ�
            float currentY = colliderTransform.position.y;
            float currentZ = colliderTransform.position.z;

            // originalText �̒����ɉ����ăR���C�_�[�� x ���W��ύX
            float newX = colliderTransform.position.x; // �f�t�H���g�͌��݂� x ���W��ێ�

            if (textLength == 7)
            {
                newX = -44.9921f;
            }
            else if (textLength == 8)
            {
                newX = -44.9478f;
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
