using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public sealed class PixelEraser : MaterialController<Image>
{
    [SerializeField, Range(0, 1)] private float _progress;

    public float Progress
    {
        get => _progress;
        set
        {
            float newProgress = Mathf.Clamp(value, 0, 1);

            if (_progress == newProgress) return;

            _progress = newProgress;

            UpdateMaterialParameters();
        }
    }

    protected override void Initialize()
    {
        base.Initialize();

        Component.material = MaterialInstance;
    }

    protected override void UpdateMaterialParameters() => MaterialInstance.SetFloat("_Progress", _progress);
}