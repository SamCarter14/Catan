using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die
{
    private int sideUp; 
    private int numSides; 
    private const int defaultNumSides = 6;
    private const int defaultSideUp = 1;

    public Die()
    {
        this.numSides = defaultNumSides;
        this.sideUp = defaultSideUp;
    }

    public int getSideUp()
    {
        return this.sideUp;
    }

    public void rollDie()
    {
        this.sideUp = Random.Range(1, 6);
    }
}
