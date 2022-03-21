using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController8 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button nextCarBtn;
    public Button prevCarBtn;
    public int carIndex;
    public Image carImage;
	void Start()
    {
        updateCarSelector();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            SceneManager.LoadScene("7");
        });
        nextCarBtn.onClick.AddListener( () => {
            Debug.Log("next");
            carIndex++;
            updateCarSelector();
        });
        prevCarBtn.onClick.AddListener( () => {
            Debug.Log("prev");
            carIndex--;
            updateCarSelector();
        });
	}
    void updateCarSelector()
    {
        if (carIndex > 6)
        {
            carIndex = 1;
        }
        if (carIndex < 1)
        {
            carIndex = 6;
        }
        carImage.sprite = Resources.Load<Sprite>("Car/Pics/" + carIndex);
    }
}