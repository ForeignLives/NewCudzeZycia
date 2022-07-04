using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomNumbers : MonoBehaviour
{
    public float BootsColor, BootsColorMultiply, ChestColor, ChestColorMultiply, PantsColor, PantsColorMultiply, HairColor, HairColorMultiply = 0f;
    public ObjectHolder BootsGenerator, ChestGenerator, Pants1Generator, Pants2Generator, Arm1Generator, Arm2Generator, HeadGenerator, HairGenerator;
    public int BootsNumber, ChestNumber, Pants1Number, Pants2Number, Arm1Number, Arm2Number, HeadNumber, HairNumber;
    void Start()
    {
        BootsColor = Random.Range(0.0f, 1.0f);
        BootsColorMultiply = Random.Range(0.5f, 1.0f);
        ChestColor = Random.Range(0.0f, 1.0f);
        ChestColorMultiply = Random.Range(0.5f, 1.0f);
        PantsColor = Random.Range(0.0f, 1.0f);
        PantsColorMultiply = Random.Range(0.5f, 1.0f);
        HairColor = Random.Range(0.0f, 1.0f);
        HairColorMultiply = Random.Range(0.5f, 1.0f);

        BootsNumber = Random.Range(0, BootsGenerator.HoldObjects.Length);
        ChestNumber = Random.Range(0, ChestGenerator.HoldObjects.Length);
        Pants1Number = Random.Range(0, Pants1Generator.HoldObjects.Length);
        Pants2Number = Random.Range(0, Pants2Generator.HoldObjects.Length);
        Arm1Number = Random.Range(0, Arm1Generator.HoldObjects.Length);
        Arm2Number = Random.Range(0, Arm2Generator.HoldObjects.Length);
        HeadNumber = Random.Range(0, HeadGenerator.HoldObjects.Length);
        HairNumber = Random.Range(0, HairGenerator.HoldObjects.Length);
    }

 
}
