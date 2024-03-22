using UnityEngine;

public class ObjectMethodCallerTest : MonoBehaviour
{
    public string someText;
    public ObjectMethodCaller methodCaller = new ObjectMethodCaller("ClearText", "RandomNumber", "SayHello");

    public void SayHello() => someText = "Hello!";
    public void RandomNumber() => someText = Random.Range(0, 10000).ToString();
    public void ClearText() => someText = "";
}
