using UnityEngine;

public static class SpeedCalculator
{
    
    public static float CalculateSpeed(float currentSpeed,float maxSpeed,float speedAcceleration)
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed = currentSpeed + Time.deltaTime * speedAcceleration;
        }
        else if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        return currentSpeed;
    }
}