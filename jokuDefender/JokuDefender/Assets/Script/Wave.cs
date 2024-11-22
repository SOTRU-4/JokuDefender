using UnityEngine;

public abstract class Wave : MonoBehaviour
{
    
    public int currentWave {  get; private set; }
    public int wavePoints { get; private set; }

    protected bool preWaveState;
    int addPointPerWave;

    protected int GetNewWavePoints()
    {
        wavePoints = 5;
        addPointPerWave += 3;
        if (!preWaveState && currentWave % 3 == 0)
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
