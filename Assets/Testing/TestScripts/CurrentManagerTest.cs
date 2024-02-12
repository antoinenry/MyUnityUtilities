using UnityEngine;

[ExecuteAlways]
public class CurrentManagerTest : MonoBehaviour
{
    public enum Mode { GET, SET }

    public Mode mode;
    public Material currentMaterial;
    public CurrentToggleAttributeTest currentScriptable;

    private void Update()
    {
        if (mode == Mode.GET)
        {
            currentScriptable = CurrentAssetsManager.GetCurrent<CurrentToggleAttributeTest>();
            currentMaterial = CurrentAssetsManager.GetCurrent<Material>();
        }
        else
        {
            CurrentAssetsManager.SetCurrent(currentScriptable);
            CurrentAssetsManager.SetCurrent(currentMaterial);
        }
    }
}