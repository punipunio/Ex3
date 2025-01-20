using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerLR : MonoBehaviour
{
    static InputManagerLR m_instance;

    // ���Ƃ�Inspector��ActionAsset����Input Action��������
    [SerializeField]
    InputActionAsset m_actionAsset;

    InputActionMap m_ActionMapR;
    InputActionMap m_ActionMapL;
    InputAction m_PrimaryButtonR;
    InputAction m_PrimaryButtonL;
    InputAction m_SecondaryButtonR;
    InputAction m_SecondaryButtonL;

    private void Awake()
    {
        m_instance = this;
        // InputAction�}�l�[�W���[���V�[������j�����Ȃ��悤�ɂ���
        GameObject.DontDestroyOnLoad(gameObject);
        // Action Maps�̖��O������
        m_ActionMapR = m_actionAsset.FindActionMap("XRI RightHand Interaction"); 
        m_ActionMapL = m_actionAsset.FindActionMap("XRI LeftHand Interaction");
        // Actions�̖��O�����ׂē����
        m_PrimaryButtonR = m_ActionMapR.FindAction("A Button", throwIfNotFound: true);
        m_SecondaryButtonR = m_ActionMapR.FindAction("B Button", throwIfNotFound: true);
        m_PrimaryButtonL = m_ActionMapL.FindAction("X Button", throwIfNotFound: true);
        m_SecondaryButtonL = m_ActionMapL.FindAction("Y Button", throwIfNotFound: true);
    }

    private void OnEnable()
    {
        m_ActionMapR?.Enable();
        m_ActionMapL?.Enable();
    }

    private void OnDisable()
    {
        m_ActionMapR?.Disable();
        m_ActionMapL?.Disable();
    }

    public static bool PrimaryButtonR()
    {
        return m_instance.m_PrimaryButtonR.IsPressed();
    }

    public static bool PrimaryButtonR_OnPress()
    {
        return m_instance.m_PrimaryButtonR.WasPressedThisFrame();
    }

    public static bool PrimaryButtonR_OnRelease()
    {
        return m_instance.m_PrimaryButtonR.WasReleasedThisFrame();
    }

    public static bool SecondaryButtonR()
    {
        return m_instance.m_SecondaryButtonR.IsPressed();
    }

    public static bool SecondaryButtonR_OnPress()
    {
        return m_instance.m_SecondaryButtonR.WasPressedThisFrame();
    }

    public static bool SecondaryButtonR_OnRelease()
    {
        return m_instance.m_SecondaryButtonR.WasReleasedThisFrame();
    }
    public static bool PrimaryButtonL()
    {
        return m_instance.m_PrimaryButtonL.IsPressed();
    }

    public static bool PrimaryButtonL_OnPress()
    {
        return m_instance.m_PrimaryButtonL.WasPressedThisFrame();
    }

    public static bool PrimaryButtonL_OnRelease()
    {
        return m_instance.m_PrimaryButtonL.WasReleasedThisFrame();
    }

    public static bool SecondaryButtonL()
    {
        return m_instance.m_SecondaryButtonL.IsPressed();
    }

    public static bool SecondaryButtonL_OnPress()
    {
        return m_instance.m_SecondaryButtonL.WasPressedThisFrame();
    }

    public static bool SecondaryButtonL_OnRelease()
    {
        return m_instance.m_SecondaryButtonL.WasReleasedThisFrame();
    }
}

// �ȉ�PrimaryButtonL�ASecondaryButtonR�ASecondaryButtonL�ł����l�ɋL��