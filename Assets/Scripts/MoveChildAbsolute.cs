using UnityEngine;

public class MoveChildAbsolute : MonoBehaviour
{
    public Transform childObject;  // �q�I�u�W�F�N�g��Transform
    public Vector3 newWorldPosition;  // �ړ���̃��[���h���W

    void Update()
    {
        if (childObject != null)
        {
            // �q�I�u�W�F�N�g���΍��W�Ɉړ�
            childObject.position = newWorldPosition;
        }
    }
}
