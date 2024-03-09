using Assets.Scripts.G3;
using System.Collections;
using UnityEngine;

public abstract class TankController : MonoBehaviour, ITankControls
{
    protected CodeManager cm;

    private Grid grid;
    private Turret turret;
    private int currentStep = 0;

    // Start is called before the first frame update
    public void Assign()
    {
        grid = GetComponentInParent<Grid>();
        turret = GetComponentInChildren<Turret>();
        cm = GameObject.Find("Code").GetComponent<CodeManager>();
    }

    public void MoveForward()
    {
        Vector3 translation = new()
        {
            x = -(int)Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z),
            y = (int)Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z)
        };

        if (!grid.CanMove(translation + transform.localPosition)) return;

        transform.Translate(translation, Space.World);
        currentStep++;

        grid.Moved(transform.localPosition);
    }

    public void MoveBackward()
    {
        Vector3 translation = new()
        {
            x = (int)Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z),
            y = -(int)Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z)
        };

        if (!grid.CanMove(translation + transform.localPosition)) return;

        transform.Translate(translation, Space.World);
        currentStep++;

        grid.Moved(transform.localPosition);
    }

    public void RotateLeft()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        currentStep++;
    }

    public void RotateRight()
    {
        transform.Rotate(new Vector3(0, 0, -90));
        currentStep++;
    }

    public void SpinTurret(float angle)
    {
        turret.transform.Rotate(new Vector3(0, 0, -angle));
        currentStep++;
    }

    public void Fire()
    {
        turret.Fire(transform.localPosition);
    }

    public abstract IEnumerator Run();

    public IEnumerator Wait()
    {
        if (cm.GetStepping())
        {
            yield return new WaitUntil(StepWait);
        }
        else
        {
            yield return new WaitForSeconds(cm.period);
        }
    }

    bool StepWait()
    {
        return cm.GetStep() >= currentStep;
    }
}
