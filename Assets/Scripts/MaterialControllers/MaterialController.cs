using UnityEngine;

public abstract class MaterialController<TComponent>
    : MonoBehaviour
    where TComponent : MonoBehaviour
{
    [field: SerializeField] protected Material Material { get; private set; }

    protected Material MaterialInstance { get; private set; }

    protected TComponent Component { get; private set; }

    private void Awake()
    {
        Initialize();

        UpdateMaterialParameters();
    }

    protected virtual void Initialize()
    {
        MaterialInstance = Instantiate(Material);
        Component = GetComponent<TComponent>();
    }

    protected abstract void UpdateMaterialParameters();
}