using UnityEngine;

public interface IActiveKeyListener
{
    void OnKeyUpdate(GameObject source, string signal, float value);
}