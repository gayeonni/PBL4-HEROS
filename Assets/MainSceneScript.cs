using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneScript : MonoBehaviour
{
    public TMP_InputField policeInputField;
    public Toggle subwayToggle;

    public void StartSimulation()
    {
        // 'Simulation Option' 팝업창에서 입력한 값을 가져와서 변수에 저장
        int policeCount = int.Parse(policeInputField.text);
        bool subwayNoStopping = subwayToggle.isOn;

        // 'SimulationScene'로 전달할 값을 PlayerPrefs에 저장
        PlayerPrefs.SetInt("PoliceCount", policeCount);
        PlayerPrefs.SetInt("SubwayNoStopping", subwayNoStopping ? 1 : 0); //무정차 옵션 선택시 0

        // 'SimulationScene'으로 이동
        SceneManager.LoadScene("SimulationScene");
    }
}
