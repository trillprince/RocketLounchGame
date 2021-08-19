using UnityEngine;

public static class SpeedCalculator
{
    
    public static void CalculateSpeed (ref float currentSpeed,float maxSpeed,float speedAcceleration)
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed = currentSpeed + Time.deltaTime * speedAcceleration;
        }
        else if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }
}