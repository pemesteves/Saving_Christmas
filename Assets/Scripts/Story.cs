using UnityEngine;

public class Story : MonoBehaviour
{
    [SerializeField]
    private GameController gameController = null;

    private void FinishStory()
    {
        gameController.LoadScene("Menu");
    }
}
