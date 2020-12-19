using TMPro;
using UnityEngine;

public class ReindeerNumber : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textNum = null;

    public void SetNumReindeers(int num)
    {
        textNum.text = num.ToString();
    }
}
