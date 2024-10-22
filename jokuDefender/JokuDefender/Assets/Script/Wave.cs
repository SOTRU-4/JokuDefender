using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{
    
    public int currentWave {  get; private set; }
    public int wavePoints { get; private set; }

    protected bool preWaveState;
    int addPointPerWave;

    protected int GetNewWavePoints()
    {
        wavePoints = 10;
        addPointPerWave += 3;
        if (currentWave % 5 == 0)
        {
            addPointPerWave += 5;
        }

        wavePoints += currentWave + addPointPerWave;
        
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
