using UnityEngine;

[ExecuteAlways]
public class CurrentManagerTest : MonoBehaviour
{
    public Material currentMaterial;
    public bool setCurrent;

    private void Update()
    {
        if (setCurrent)
            CurrentAssetsManager.SetCurrent(currentMaterial);
        else
            currentMaterial = CurrentAssetsManager.GetCurrent<Material>();
    }
}