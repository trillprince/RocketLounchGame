using Common.Scripts.Input;
using UnityEngine;

public class InputFactory
{
    private TouchControls _touchControls;
    public InputFactory()
    {
        _touchControls = new TouchControls();
    }

    public IInputPlatform BuildPlatform(RuntimePlatform runtimePlatform)
    {
        switch (runtimePlatform)
        {
            case RuntimePlatform.Android:
                return new AccelerometerInput(_touchControls);
            case RuntimePlatform.WindowsEditor:
                return new EditorInput(_touchControls);
        }
        return default;
    }
}