using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class ResetSimulator : MonoBehaviour
{
    void Start()
    {
        var sim = FindFirstObjectByType<XRInteractionSimulator>();
        if (sim != null)
        {
            sim.enabled = false;
            sim.enabled = true;
        }
    }
}