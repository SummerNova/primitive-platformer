using UnityEngine;

public class MinimapAgent : MonoBehaviour
{
    [SerializeField] RenderType renderType;
    private int RenderID;

    private void OnEnable()
    {
        RenderID = MinimapManager.LoadObject(transform, renderType);
    }

    private void OnDisable()
    {
        MinimapManager.UnloadObject(RenderID,renderType);
    }
}
