using UnityEngine;

[RequireComponent(typeof(Light))]
public class SetLuxIntensity : MonoBehaviour
{
    public float luxValue = 300; // ルクス値の設定

    private Light lightComponent;

    void Awake()
    {
        lightComponent = GetComponent<Light>();
        UpdateLightIntensity();
    }

    // ライトの強度を更新するメソッド
    private void UpdateLightIntensity()
    {
        if (lightComponent != null)
        {
            lightComponent.intensity = luxValue * 0.007f;
        }
    }

    // luxValueが変更されたときに反映
    private void OnValidate()
    {
        if (lightComponent == null)
        {
            lightComponent = GetComponent<Light>();
        }
        UpdateLightIntensity();
    }
}
