using UnityEngine;

public class MissionTarget : MonoBehaviour
{
    public GameObject target;
    public GameObject arrow;

    public bool activeTarget;
    public Material targetMaterial;

    private Material _originalMaterial;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = target.GetComponent<Renderer>();
        _originalMaterial = _renderer.material;
    }

    private void Update()
    {
        _renderer.material = activeTarget
            ? targetMaterial
            : _originalMaterial;

        arrow.SetActive(activeTarget);
    }
}
