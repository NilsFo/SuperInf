using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sharpness
{
    Dull,
    Normal,
    Burning
}
public class Sharp : MonoBehaviour
{
    public Sharpness sharpness = Sharpness.Normal;

}
