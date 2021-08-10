using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardConfig", menuName = "ScriptableObjects/BoardConfig", order = 1)]
public class BoardConfig : ScriptableObject
{

    public Sprite[] numbersImages;
    public double bombProbability;
    public Sprite bomb;
    public Sprite flag;
    public Sprite defaultImage;
}
