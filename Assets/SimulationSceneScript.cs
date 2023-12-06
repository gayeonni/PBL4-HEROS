using UnityEngine;
using UnityEngine.UI;

public class SimulationSceneScript : MonoBehaviour
{
    public Text policeCountText;
    public Text subwayNoStoppingText;

    void Start()
    {
        // 'SimulationScene'에서 PlayerPrefs에서 값을 가져와서 Text UI에 표시
        int policeCount = PlayerPrefs.GetInt("PoliceCount", 0);
        bool subwayNoStopping = PlayerPrefs.GetInt("SubwayNoStopping", 0) == 1;

        policeCountText.text = policeCount.ToString();
        subwayNoStoppingText.text = subwayNoStopping ? "무정차" : "정차";
    }
}