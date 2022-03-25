using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrbBox : MonoBehaviour
{

    public static readonly double movingTime = .15f;
    public bool xStricted
    {
        get
        {
            return ((((int)(transform.localPosition.x * 100)) / 100.0) % 1) == 0;
        }
    }

    public bool yStricted
    {
        get
        {
            return ((((int)(transform.localPosition.y * 100)) / 100.0) % 1) == 0;
        }
    }
    public Orb Orb;

    public bool lying
    {
        get
        {
            if ((xStricted && (int)(this.transform.localPosition.y) == 0)) return true;
            else if (((int)this.transform.localPosition.y >= MixingBoard.Height) || falling) return false;
            else if (xStricted && yStricted && (int)(this.transform.localPosition.y) > 0 && MixingBoard.StaticInstance.orbs[(int)this.transform.localPosition.x, (int)(this.transform.localPosition.y - 1)] != null) return true;
            else return false;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(fallingCheck());
    }

    //используется, чтобы сфера не начинала падать, пока уже падает
    public bool falling = false;
    private void Start()
    {
        Orb = GetComponentInChildren<Orb>();
    }

    IEnumerator fallingCheck()
    {
        while (true)
        {
            if(transform.parent == null) yield return new WaitForSeconds(.07f);

            if (transform.parent.gameObject == MixingBoard.StaticInstance.OrbShift)
            {
                if (!xStricted || !yStricted || !lying) MixingBoard.StaticInstance.spinDelay = Math.Max(.1, MixingBoard.StaticInstance.spinDelay);

                if (!falling)
                {
                    if (transform.localPosition.y > 0 && xStricted && MixingBoard.StaticInstance.stricted && (int)transform.localPosition.y < MixingBoard.Height)
                    {
                        int fallDistance = 0;
                        for (int i = 1; i <= transform.localPosition.y; i++)
                        {
                            try
                            {
                                if (MixingBoard.StaticInstance.orbs[(int)transform.localPosition.x, (int)transform.localPosition.y - i] == null)
                                {
                                    fallDistance++;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Debug.LogError("IndexOutOfRangeException " + (int)transform.localPosition.x + " " + (int)(transform.localPosition.y - i));
                            }

                        }
                        if (fallDistance != 0 && MixingBoard.StaticInstance.stricted)
                        {
                            MixingBoard.StaticInstance.currentTargetDelay = MixingBoard.StaticInstance.targetDelay * fallDistance;
                            for (int j = (int)transform.localPosition.y; j < MixingBoard.Height; j++)
                            {
                                if (MixingBoard.StaticInstance.orbs[(int)transform.localPosition.x, j] != null)
                                {
                                    if (MixingBoard.StaticInstance.orbs[(int)transform.localPosition.x, j].channeling) MixingBoard.StaticInstance.breakReactionsWith(MixingBoard.StaticInstance.orbs[(int)transform.localPosition.x, j]);
                                    iTween.MoveTo(MixingBoard.StaticInstance.orbs[(int)transform.localPosition.x, j].Box.gameObject, iTween.Hash("position", new Vector3(MixingBoard.StaticInstance.orbs[(int)transform.localPosition.x, j].Box.transform.localPosition.x, MixingBoard.StaticInstance.orbs[(int)transform.localPosition.x, j].Box.transform.localPosition.y - fallDistance, MixingBoard.StaticInstance.orbs[(int)transform.localPosition.x, j].Box.transform.localPosition.z), "islocal", true, "time", movingTime * fallDistance, "easetype", iTween.EaseType.easeInOutSine));
                                    MixingBoard.StaticInstance.spinDelay = Math.Max(movingTime * fallDistance, MixingBoard.StaticInstance.spinDelay);
                                    falling = true;
                                    Invoke("fallingReset", Convert.ToSingle(movingTime * fallDistance));
                                }
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(.07f);
        }
    }

    private void Update()
    {
        
    }
    public void fallingReset()
    {
        falling = false;
    }
    public static bool operator true(OrbBox orb)
    {
        return orb != null;
    }

    public static bool operator false(OrbBox orb)
    {
        return orb == null;
    }

    public void moveDown()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 1, this.transform.localPosition.z), "islocal", true, "time", movingTime, "easetype", iTween.EaseType.easeInSine));
    }

    public void moveUp()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 1, this.transform.localPosition.z), "islocal", true, "time", movingTime, "easetype", iTween.EaseType.easeInSine));
    }

    public void moveRight()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x + 1, this.transform.localPosition.y, this.transform.localPosition.z), "islocal", true, "time", movingTime, "easetype", iTween.EaseType.easeInSine));
    }

    public void moveLeft()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x - 1, this.transform.localPosition.y, this.transform.localPosition.z), "islocal", true, "time", movingTime, "easetype", iTween.EaseType.easeInSine));
    }

    public void moveRightCombine()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x + 1, this.transform.localPosition.y, this.transform.localPosition.z), "islocal", true, "time", Orb.destroyingTimer * 2, "easetype", iTween.EaseType.easeInOutBack));

    }

    public void moveLeftCombine()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x - 1, this.transform.localPosition.y, this.transform.localPosition.z), "islocal", true, "time", Orb.destroyingTimer * 2, "easetype", iTween.EaseType.easeInOutBack));
    }

    public void moveUpCombine()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 1, this.transform.localPosition.z), "islocal", true, "time", Orb.destroyingTimer * 2, "easetype", iTween.EaseType.easeInOutBack));
    }

    public void moveDownCombine()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 1, this.transform.localPosition.z), "islocal", true, "time", Orb.destroyingTimer * 2, "easetype", iTween.EaseType.easeInOutBack));
    }
}
