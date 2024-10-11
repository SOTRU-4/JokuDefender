using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{
    
    public int currentWave {  get; private set; }
    public int wavePoints { get; private set; }

    protected bool preWaveState {  get; set; }

    protected int GetNewWavePoints()
    {
        wavePoints = 10;
        wavePoints += currentWave + currentWave;
        if(currentWave % 5 == 0) 
        {
            wavePoints += 10;
        }
        return wavePoints;
    }
    protected void NextWave()
    {
        if (!preWaveState) 
        {
            currentWave++;
        }
        preWaveState = !preWaveState;
    }
}
