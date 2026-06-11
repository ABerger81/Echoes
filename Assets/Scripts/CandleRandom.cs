using UnityEngine;


// This script is attached to the candle in the game. It randomly activates one of three candle game objects at the start of the game. This can be used to add variety to the game by having the candle appear in different locations each time the game is played.
public class CandleRandom : MonoBehaviour
{
    [SerializeField] int candleLocation;
    [SerializeField] GameObject candle1;
    [SerializeField] GameObject candle2;
    [SerializeField] GameObject candle3;

    void Start()
    {
        candleLocation = Random.Range(1, 4);

        if (candleLocation == 1)
        {
            candle1.SetActive(true);
        }
        if (candleLocation == 2)
        {
            candle2.SetActive(true);
        }
        if (candleLocation == 3)
        {
            candle3.SetActive(true);
        }
    }
}
