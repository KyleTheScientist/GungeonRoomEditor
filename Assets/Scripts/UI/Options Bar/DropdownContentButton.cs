using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownContentButton : MonoBehaviour
{
    public bool hideParentOnClick = true;

    public virtual void OnClick()
    {
        if (hideParentOnClick)
            this.transform.parent.gameObject.SetActive(false);
    }

}
