using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSCounter))] 
public class FPSDisplay : MonoBehaviour {

    public Text highestFPSLabel, averageFPSLabel, lowestFPSLabel;
 
    // We're storing the FPS values of multiple frames
    // in a buffer, using an index to set where the put
    // data on the next frame.
    FPSCounter fpsCounter;
    int fpsBufferIndex;
    int[] fpsBuffer;

    static string[] stringsFrom00To99 = {
		"00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
		"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
		"20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
		"30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
		"40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
		"50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
		"60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
		"70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
		"80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
		"90", "91", "92", "93", "94", "95", "96", "97", "98", "99"
	};

    public int HighestFPS { get; private set; }
    public int LowestFPS { get; private set; }

    void InitializeBuffer () 
    {
        if (frameRange <= 0) {
            frameRange = 1;
        }
        fpsBuffer = new int[frameRange];
        fpsBufferIndex = 0;
    }

    void Awake () 
    {
        fpsCounter = GetComponent<FPSCounter>();
    }

    void Update () 
    {
        if (fpsBuffer == null || fpsBuffer.Length != frameRange) {
            InitializeBuffer();
        }
        UpdateBuffer();
        CalculateFPS();
    }

    void UpdateBuffer () 
    {
        fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        if (fpsBufferIndex >= frameRange) {
            fpsBufferIndex = 0;
        }
    }

    void CalculateFPS ()
    {
        int sum = 0;
        int highest = 0;
        int lowest = int.MaxValue;

        for (int i = 0; i < frameRange; i++) {
            int fps = fpsBuffer[i];
            sum += fps;

            if (fps > highest) {
                highest = fps;
            }

            if (fps < lowest) {
                lowest = fps;
            }
        }
        
        AverageFPS = sum / frameRange;
        HighestFPS = highest;
        LowestFPS = lowest;
    }
}
