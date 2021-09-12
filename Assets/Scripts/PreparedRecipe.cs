using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparedRecipe : MonoBehaviour
{
    [SerializeField]
    public GameObject recipeIcon;
    [SerializeField]
    public GameObject counter;

    public Recipe recipe;

    public void unprepare()
    {
        CookingModule.unprepareRecipe(this);
    }

    public void dissolveIn(float time)
    {
        IDissolving[] elems = GetComponentsInChildren<IDissolving>(true);
        foreach (IDissolving elem in elems)
        {
            elem.disappear();
        }
        Invoke("destroy", time);
    }

    private void destroy()
    {
        Destroy(this.gameObject);
    }
}
