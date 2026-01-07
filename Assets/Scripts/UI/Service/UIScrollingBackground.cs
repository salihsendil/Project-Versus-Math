using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class UIScrollingBackground : MonoBehaviour
{
    private RawImage rawImage;
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0.025f, 0.02f);

    private Rect uvRect;

    private void Awake()
    {
        TryGetComponent(out rawImage);
        uvRect = rawImage.uvRect;
    }

    private void Update()
    {
        uvRect.position += scrollSpeed * Time.deltaTime;
        rawImage.uvRect = uvRect;
    }
}
