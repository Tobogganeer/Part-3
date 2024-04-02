using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalUI : MonoBehaviour
{
    public Image productImage;
    public TMP_Text productCountText;
    public TMP_Text productNameText;

    public void Set(Product target, int currentCount)
    {
        productImage.sprite = target.Sprite;
        productCountText.text = $"{currentCount}/{target.Amount}";
        productNameText.text = target.ID.ToString(); // Some names will be StuckTogether but oh well
    }
}
