using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public ObjectHolder _OH;
    public GenerateRandomNumbers GRN;
    public bool Boots, Chest, Arm1, Arm2, Pants1, Pants2, Head, Hair;

    void Start()
    {
        if(Boots)
        {
            Instantiate (_OH.HoldObjects[GRN.BootsNumber], transform).AddComponent<RandomColorObject>();
        }
        else if(Pants2)
        {
            Instantiate(_OH.HoldObjects[GRN.Pants2Number], transform).AddComponent<RandomColorObject>();
        }
        else if (Pants1)
        {
            Instantiate(_OH.HoldObjects[GRN.Pants1Number], transform).AddComponent<RandomColorObject>();
        }
        else if (Arm2)
        {
            Instantiate(_OH.HoldObjects[GRN.Arm2Number], transform).AddComponent<RandomColorObject>();
        }
        else if (Arm1)
        {
            Instantiate(_OH.HoldObjects[GRN.Arm1Number], transform).AddComponent<RandomColorObject>();
        }
        else if (Chest)
        {
            Instantiate(_OH.HoldObjects[GRN.ChestNumber], transform).AddComponent<RandomColorObject>();
        }
        else if (Head)
        {
            Instantiate(_OH.HoldObjects[GRN.HeadNumber], transform);
        }
        else if (Hair)
        {
            Instantiate(_OH.HoldObjects[GRN.HairNumber], transform).AddComponent<RandomColorObject>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
