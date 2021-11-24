using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public RawImage compassScrollTexture;
    public Transform playersPosInWorld;

    private void Update()
    {
        compassScrollTexture.uvRect = new Rect(playersPosInWorld.localEulerAngles.y / 360, 0, 1, 1);
    }
}
