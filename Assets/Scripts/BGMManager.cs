using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ƒV[ƒ“‚ªØ‚è‘Ö‚í‚Á‚Ä‚à”jŠü‚µ‚È‚¢
        }
        else
        {
            Destroy(gameObject); // d•¡‚ğ–h‚®‚½‚ßŠù‘¶‚ÌBGMManager‚ª‚ ‚éê‡‚Ííœ
        }
    }
}
