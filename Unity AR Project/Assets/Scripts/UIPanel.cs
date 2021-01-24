using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIPanel : MonoBehaviour
{
    public bool open;
    public float openX, openY, openZ;
    public GameObject button;

    public void OpenClosePanel(GameObject panel)
    {
        float currentX = panel.transform.localPosition.x;
        float currentY = panel.transform.localPosition.y;
        float currentZ = panel.transform.localPosition.z;

        if (open)
        {
            panel.transform.DOLocalMove(new Vector3(currentX + openX, currentY + openY, currentZ + openZ), 0.5f);
            open = false;

            if (button != null)
            {
                //button.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.5f, RotateMode.Fast);
                button.transform.DOScaleY(1, 0.5f);
            }
        }
        else
        {
            panel.transform.DOLocalMove(new Vector3(currentX - openX, currentY - openY, currentZ - openZ), 0.5f);
            open = true;

            if (button != null)
            {
                //button.transform.DOLocalRotate(new Vector3(0, 0, 90), 0.5f, RotateMode.Fast);
                button.transform.DOScaleY(-1, 0.5f);
            }
        }

    }

}
