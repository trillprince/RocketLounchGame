using Common.Scripts.Infrastructure;
using Common.Scripts.Input;
using UnityEngine;
using AndroidInput = Common.Scripts.Input.AndroidInput;

public class InputFactory
{
    private TouchControls _touchControls;
    private ICoroutineRunner _coroutineRunner;

    public InputFactory(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
        _touchControls = new TouchControls();
    }

    public IInputPlatform BuildPlatform(RuntimePlatform runtimePlatform)
    {
        switch (runtimePlatform)
        {
            case RuntimePlatform.Android:
                return new AndroidInput(_touchControls);
            case RuntimePlatform.WindowsEditor:
                return new EditorInput(_touchControls,_coroutineRunner);
        }
        return default;
    }
}