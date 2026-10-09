using UnityEngine;

public class HUDTestData : MonoBehaviour
{
    public HUDController hud;

    void Update()
    {
        float speed = 250f + Mathf.Sin(Time.time) * 100f;
        float altitude = 5000f + Mathf.Sin(Time.time * 0.5f) * 1000f;
        float verticalSpeed = Mathf.Sin(Time.time * 2f) * 50f;
        float fuel = Mathf.Max(0f, 100f - Time.time * 2f);
        float gForce = 1f + Mathf.Abs(Mathf.Sin(Time.time * 1.5f)) * 2f;

        if (hud != null)
        {
            hud.SetSpeed(speed);
            hud.SetAltitude(altitude);
            hud.SetVerticalSpeed(verticalSpeed);
            hud.SetFuel(fuel);
            hud.SetGForce(gForce);
        }
    }
}