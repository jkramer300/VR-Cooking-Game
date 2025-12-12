using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipShader
{
    // factor: changes how much % of the object is rendered  0 == nothing 1 == everything
    // capsize: size of the cap (min, max) size 
    // capHeight: height of the cap

    /// <summary>
    /// INT
    /// </summary>
    /// <param name="factor"> changes how much % of the object is rendered  0 == nothing 1 == everything </param>
    /// <param name="capsizeX"> changes how much % of the object is rendered  0 == nothing 1 == everything </param>
    /// <param name="capsizeY"> changes how much % of the object is rendered  0 == nothing 1 == everything </param>
    /// <param name="capsizeZ"> changes how much % of the object is rendered  0 == nothing 1 == everything </param>
    /// <param name="capHeight"> changes how much % of the object is rendered  0 == nothing 1 == everything </param>
    public void ChangeShader(float factor, (float, float) capSizeX, (float, float) capSizeY, (float, float) capSizeZ, (float, float) capHeight, GameObject _base, Material material, int rotation = 1, float sizeX = 0, float sizeY = 0, float sizeZ = 0)
    {
        if (sizeX == 0)
            sizeX = capSizeX.Item1 + (capSizeX.Item2 - capSizeX.Item1) * factor;
        if (sizeY == 0)
            sizeY = capSizeY.Item1 + (capSizeY.Item2 - capSizeY.Item1) * factor;
        if (sizeZ == 0)
            sizeZ = capSizeZ.Item1 + (capSizeZ.Item2 - capSizeZ.Item1) * factor;
        _base.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);

        float height = capHeight.Item1 + (capHeight.Item1 - capHeight.Item2) * factor * rotation;
        _base.transform.localPosition = new Vector3(0, 0, height);

        float pos = material.GetFloat("_Min") + ((material.GetFloat("_Max") - material.GetFloat("_Min")) * factor);
        material.SetFloat("_RevealAmount", pos);
    }
}
