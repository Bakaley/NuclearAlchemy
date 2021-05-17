using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[CustomEditor(typeof(Ingredient))]
[CanEditMultipleObjects]
public class IngredientEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate preview"))
        {
            IngredientPlace ingredientPlace = GameObject.Find("IngredientPlace4").GetComponent<IngredientPlace>();
            GameObject IngredientPreviewSample = Resources.Load("IngredientPreviewSampler") as GameObject;

            List<Ingredient> ingredients = new List<Ingredient>();

            foreach (System.Object ingredient in targets)
            {
                ingredients.Add((Ingredient)ingredient);
            }
            foreach (Ingredient ingredient in ingredients)
            {
                List<Orb> orbs = new List<Orb>(ingredient.GetComponentsInChildren<Orb>());
                GameObject ingredientPreview = Instantiate(IngredientPreviewSample, ingredientPlace.transform);

                SpriteRenderer essense1 = null;
                SpriteRenderer essense2 = null;
                GameObject orbShift = null;

                foreach (Transform child in ingredientPreview.transform)
                {
                    if (child.gameObject.name == "Essense1") essense1 = child.gameObject.GetComponent<SpriteRenderer>();
                    if (child.gameObject.name == "Essense2") essense2 = child.gameObject.GetComponent<SpriteRenderer>();
                    if (child.gameObject.name == "IngredientOrbShift") orbShift = child.gameObject;
                }

                ingredientPreview.GetComponent<IngredientPreview>().ingredient = ingredient.GetComponent<Ingredient>().gameObject;

                if (ingredient.essence1 == Ingredient.ESSENSE.None)
                {
                    essense1.enabled = false;
                    essense2.enabled = false;
                }
                else if (ingredient.essence2 == Ingredient.ESSENSE.None)
                {
                    essense1.transform.localPosition = new Vector3(0, essense1.transform.localPosition.y, essense1.transform.localPosition.z);
                    GameObject essenceIcon1 = Resources.Load("EssenceIcons/" + ingredient.essence1.ToString() + "Essence") as GameObject;
                    essense1.sprite = essenceIcon1.GetComponent<SpriteRenderer>().sprite;
                    essense1.color = essenceIcon1.GetComponent<SpriteRenderer>().color;
                    essense2.enabled = false;
                }
                else
                {
                    GameObject essenceIcon1 = Resources.Load("EssenceIcons/" + ingredient.essence1.ToString() + "Essence") as GameObject;
                    essense1.sprite = essenceIcon1.GetComponent<SpriteRenderer>().sprite;
                    essense1.color = essenceIcon1.GetComponent<SpriteRenderer>().color;

                    GameObject essenceIcon2 = Resources.Load("EssenceIcons/" + ingredient.essence2.ToString() + "Essence") as GameObject;
                    essense2.sprite = essenceIcon2.GetComponent<SpriteRenderer>().sprite;
                    essense2.color = essenceIcon2.GetComponent<SpriteRenderer>().color;
                }

                int minX = (int)Math.Round(orbs[0].gameObject.transform.localPosition.x);
                int maxX = (int)Math.Round(orbs[0].gameObject.transform.localPosition.x);
                int minY = (int)Math.Round(orbs[0].gameObject.transform.localPosition.y);
                int maxY = (int)Math.Round(orbs[0].gameObject.transform.localPosition.y);

                foreach (Orb orb in orbs)
                {
                    if ((int)orb.gameObject.transform.localPosition.x < minX) minX = (int)Math.Round(orb.gameObject.transform.localPosition.x);
                    if ((int)orb.gameObject.transform.localPosition.x > maxX) maxX = (int)Math.Round(orb.gameObject.transform.localPosition.x);
                    if ((int)orb.gameObject.transform.localPosition.y < minY) minY = (int)Math.Round(orb.gameObject.transform.localPosition.y);
                    if ((int)orb.gameObject.transform.localPosition.y > maxY) maxY = (int)Math.Round(orb.gameObject.transform.localPosition.y);
                }

                int length = maxX - minX + 1;
                int heigth = maxY - minY + 1;

                float xShift;
                float yShift;

                if (length % 2 == 1) xShift = .5f;
                else xShift = 0;

                if (heigth == 1) yShift = 1.5f;
                else if (heigth == 2) yShift = 1f;
                else if (heigth == 3) yShift = .5f;
                else yShift = 0;


                foreach (Orb orb in orbs)
                {
                    GameObject _orb = Instantiate(orb.OrbPreview, new Vector3((orb.transform.localPosition.x + xShift), (orb.transform.localPosition.y + yShift), 0), Quaternion.identity);
                    _orb.transform.SetParent(orbShift.transform, false);
                }

                ingredient.gameObject.name = ingredient.IngredientName;

                PrefabUtility.SaveAsPrefabAsset(ingredient.gameObject, "Assets/Ingredients/Common/" + ingredient.IngredientName + ".prefab");
                PrefabUtility.SaveAsPrefabAsset(ingredientPreview, "Ingredients/Common/" + ingredient.IngredientName + "Preview.prefab");

                GameObject loadedPreview = Resources.Load("Ingredients/Common/" + ingredient.IngredientName + "Preview") as GameObject;
                GameObject loadedIngredient = Resources.Load("Ingredients/Common/" + ingredient.IngredientName) as GameObject;

                //ingredientPreview.GetComponent<IngredientPreview>().ingredient = loadedIngredient;

                loadedPreview.GetComponent<IngredientPreview>().ingredient = loadedIngredient;
                loadedIngredient.GetComponent<Ingredient>().preview = loadedPreview;
            }
        }

        /*if (GUILayout.Button("Refresh preview"))
        {
            IngredientPlace ingredientPlace = GameObject.Find("IngredientPlace4").GetComponent<IngredientPlace>();
            GameObject IngredientPreviewSample = Resources.Load("IngredientPreview/IngredientPreviewSampler") as GameObject;

            List<Ingredient> ingredients = new List<Ingredient>();

            foreach (System.Object ingredient in targets)
            {
                ingredients.Add((Ingredient)ingredient);
            }
            foreach (Ingredient ingredient in ingredients)
            {
                List<Orb> orbs = new List<Orb>(ingredient.GetComponentsInChildren<Orb>());
                GameObject ingredientPreview = Instantiate(IngredientPreviewSample, ingredientPlace.transform);

                SpriteRenderer essense1 = null;
                SpriteRenderer essense2 = null;
                GameObject orbShift = null;

                foreach (Transform child in ingredientPreview.transform)
                {
                    if (child.gameObject.name == "Essense1") essense1 = child.gameObject.GetComponent<SpriteRenderer>();
                    if (child.gameObject.name == "Essense2") essense2 = child.gameObject.GetComponent<SpriteRenderer>();
                    if (child.gameObject.name == "IngredientOrbShift") orbShift = child.gameObject;
                }

                ingredientPreview.GetComponent<IngredientPreview>().ingredient = ingredient.GetComponent<Ingredient>().gameObject;

                if (ingredient.essence1 == Ingredient.ESSENSE.None)
                {
                    essense1.enabled = false;
                    essense2.enabled = false;
                }
                else if (ingredient.essence2 == Ingredient.ESSENSE.None)
                {
                    essense1.transform.localPosition = new Vector3(0, essense1.transform.localPosition.y, essense1.transform.localPosition.z);
                    GameObject essenceIcon1 = Resources.Load("EssenceIcons/" + ingredient.essence1.ToString() + "Essence") as GameObject;
                    essense1.sprite = essenceIcon1.GetComponent<SpriteRenderer>().sprite;
                    essense1.color = essenceIcon1.GetComponent<SpriteRenderer>().color;
                    essense2.enabled = false;
                }
                else
                {
                    GameObject essenceIcon1 = Resources.Load("EssenceIcons/" + ingredient.essence1.ToString() + "Essence") as GameObject;
                    essense1.sprite = essenceIcon1.GetComponent<SpriteRenderer>().sprite;
                    essense1.color = essenceIcon1.GetComponent<SpriteRenderer>().color;

                    GameObject essenceIcon2 = Resources.Load("EssenceIcons/" + ingredient.essence2.ToString() + "Essence") as GameObject;
                    essense2.sprite = essenceIcon2.GetComponent<SpriteRenderer>().sprite;
                    essense2.color = essenceIcon2.GetComponent<SpriteRenderer>().color;
                }

                int minX = (int)orbs[0].gameObject.transform.localPosition.x;
                int maxX = (int)orbs[0].gameObject.transform.localPosition.x;
                int minY = (int)orbs[0].gameObject.transform.localPosition.y;
                int maxY = (int)orbs[0].gameObject.transform.localPosition.y;

                foreach (Orb orb in orbs)
                {
                    if ((int)orb.gameObject.transform.localPosition.x < minX) minX = (int)orb.gameObject.transform.localPosition.x;
                    if ((int)orb.gameObject.transform.localPosition.x > maxX) maxX = (int)orb.gameObject.transform.localPosition.x;
                    if ((int)orb.gameObject.transform.localPosition.y < minY) minY = (int)orb.gameObject.transform.localPosition.y;
                    if ((int)orb.gameObject.transform.localPosition.y > maxY) maxY = (int)orb.gameObject.transform.localPosition.y;

                }

                int length = maxX - minX + 1;
                int heigth = maxY - minY + 1;

                float xShift;
                float yShift;

                if (length % 2 == 1) xShift = .5f;
                else xShift = 0;

                if (heigth == 1) yShift = 1.5f;
                else if (heigth == 2) yShift = 1f;
                else if (heigth == 3) yShift = .5f;
                else yShift = 0;

                Debug.Log("length " + length + "heigth " + heigth);

                foreach (Orb orb in orbs)
                {
                    GameObject _orb = Instantiate(orb.OrbPreview, new Vector3((orb.transform.localPosition.x + xShift), (orb.transform.localPosition.y + yShift), 0), Quaternion.identity);
                    _orb.transform.SetParent(orbShift.transform, false);
                }

                ingredient.gameObject.name = ingredient.IngredientName;

                GameObject oldPreview = Resources.Load("Assets/Prefabs/Ingredients/Common/" + ingredient.IngredientName + "Preview") as GameObject;
                DestroyImmediate(oldPreview, true);
                PrefabUtility.SaveAsPrefabAsset(ingredientPreview, "Assets/Prefabs/Ingredients/Common/" + ingredient.IngredientName + "Preview.prefab");

                GameObject oldIngredient = Resources.Load("Assets/Prefabs/Ingredients/Common/" + ingredient.IngredientName) as GameObject;
                DestroyImmediate(oldIngredient, true);
                PrefabUtility.SaveAsPrefabAsset(ingredient.gameObject, "Assets/Prefabs/Ingredients/Common/" + ingredient.IngredientName + ".prefab");

                GameObject loadedPreview = Resources.Load("Assets/Prefabs/Ingredients/Common/" + ingredient.IngredientName + "Preview") as GameObject;
                GameObject loadedIngredient = Resources.Load("Assets/Prefabs/Ingredients/Common/" + ingredient.IngredientName) as GameObject;

                ingredientPreview.GetComponent<IngredientPreview>().ingredient = loadedIngredient;
                //Destroy(target);

                Debug.Log(loadedPreview);
                Debug.Log(loadedIngredient);

                loadedPreview.GetComponent<IngredientPreview>().ingredient = loadedIngredient;
                loadedIngredient.GetComponent<Ingredient>().preview = loadedPreview;
            }
        }*/
    }
}
