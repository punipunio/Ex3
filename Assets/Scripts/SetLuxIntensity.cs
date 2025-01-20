using UnityEngine;

[RequireComponent(typeof(Light))]
public class SetLuxIntensity : MonoBehaviour
{
    public float luxValue = 300; // ���N�X�l�̐ݒ�

    private Light lightComponent;

    void Awake()
    {
        lightComponent = GetComponent<Light>();
        UpdateLightIntensity();
    }

    // ���C�g�̋��x���X�V���郁�\�b�h
    private void UpdateLightIntensity()
    {
        if (lightComponent != null)
        {
            lightComponent.intensity = luxValue * 0.007f;
        }
    }

    // luxValue���ύX���ꂽ�Ƃ��ɔ��f
    private void OnValidate()
    {
        if (lightComponent == null)
        {
            lightComponent = GetComponent<Light>();
        }
        UpdateLightIntensity();
    }
}
