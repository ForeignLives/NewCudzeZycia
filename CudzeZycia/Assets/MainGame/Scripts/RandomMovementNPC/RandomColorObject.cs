using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorObject : MonoBehaviour
{
    private MaterialPropertyBlock _mat;
    private Renderer _renderer;
    public GenerateRandomNumbers GRN;
    private Generator Gen;
    void Start()
    {
        GRN = transform.root.GetComponent<GenerateRandomNumbers>();
        Gen = GetComponentInParent<Generator>();
        _mat = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();


        _renderer.GetPropertyBlock(_mat);


        if (Gen.Boots == true)
        {
            _mat.SetFloat("_RandomColor", GRN.BootsColor);
            _mat.SetFloat("_RandomMultiply", GRN.BootsColorMultiply);
        }
        else if (Gen.Pants1 == true || Gen.Pants2 == true)
        {
            _mat.SetFloat("_RandomColor", GRN.PantsColor);
            _mat.SetFloat("_RandomMultiply", GRN.PantsColorMultiply);
        }
        else if (Gen.Chest == true || Gen.Arm2 == true || Gen.Arm1 == true)
        {
            _mat.SetFloat("_RandomColor", GRN.ChestColor);
            _mat.SetFloat("_RandomMultiply", GRN.ChestColorMultiply);
        }
        else if (Gen.Hair == true)
        {
            _mat.SetFloat("_RandomColor", GRN.HairColor);
            _mat.SetFloat("_RandomMultiply", GRN.HairColorMultiply);
        }

        _renderer.SetPropertyBlock(_mat);
    }

    
}
