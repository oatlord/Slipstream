using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}