using Assets.Scripts.G3;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using TMPro;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class CodeManager : MonoBehaviour
{
    public GameObject codeText, errorText;

    public float period = 1f;

    [SerializeField]
    private int maxSteps = 100;

    [SerializeField]
    private Level level;

    [SerializeField]
    private string[] bannedKeywords = { "using, Begin" };

    private Grid grid;

    private GameObject tankobj;
    private ITankControls addedControls;
    private TMP_InputField codeInput;
    private IEnumerator coroutine;

    private string[] methods;

    private bool stepping = false;
    private int step = -1;
    private bool compiled = false, running = false;

    public void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        codeInput = codeText.GetComponent<TMP_InputField>();

        List<string> mTemp = new();
        foreach (var method in typeof(ITankControls).GetMethods())
        {
            if (method.Name == "Run") continue;
            mTemp.Add(method.Name);
        }

        methods = mTemp.ToArray();
    }

    public void OnRunAllPress()
    {
        if (!compiled || running) return;

        running = true;
        coroutine = addedControls.Run();
        StartCoroutine(coroutine);
    }

    public void OnRunStepPress()
    {
        if (!compiled || running) return;

        running = true;
        stepping = true;
        coroutine = addedControls.Run();
        StartCoroutine(coroutine);
    }

    public void OnStepPress()
    {
        if (!running || !compiled || !stepping) return;
        step++;

        if (step >= maxSteps)
        {
            ForceStop();
        }
    }

    public void OnCompilePress()
    {
        compiled = false;
        errorText.SetActive(false);

        if (running) return;

        grid.ResetGrid();

        // TODO: May need some sort of validation to prevent malicious stuff yk. Can add more banned keywords if they are malicious!
        string code = codeInput.text;

        foreach (var word in bannedKeywords)
        {
            if (code.Contains(word))
            {
                errorText.GetComponentInChildren<TMP_Text>().text = "Code Invalid! Contains Banned Keyword '" + word + "'!";
                errorText.SetActive(true);
                return;
            }
        }

        if (level != null && !level.PreCompilationValidate(code)) 
        {
            errorText.GetComponentInChildren<TMP_Text>().text = "Code Invalid! Requirements Not Met!";
            errorText.SetActive(true);
            return;
        }

        foreach (string method in methods)
        {
            code = code.Replace(method, "yield return Wait();" + method);
        }

        try
        {
            var assembly = Compile(@"
            using UnityEngine;
            using System.Collections;

            public class RuntimeCompiled : TankController
            {        
                public static RuntimeCompiled AddYourselfTo(GameObject host)
                {
                    return host.AddComponent<RuntimeCompiled>();
                }

                void Start()
                {
                    Assign();     
                    Debug.Log(""The runtime compiled component was successfully attached to"" + gameObject.name);
                }

                public override IEnumerator Run()
                {" + code + "\ncm.Stopped();yield return null;}}");

            var runtimeType = assembly.GetType("RuntimeCompiled");
            var method = runtimeType.GetMethod("AddYourselfTo");
            var del = (Func<GameObject, MonoBehaviour>)
                          Delegate.CreateDelegate(
                              typeof(Func<GameObject, MonoBehaviour>),
                              method
                      );

            // We ask the compiled method to add its component to this.gameObject
            addedControls = (ITankControls)del.Invoke(tankobj);
            compiled = true;
        } 
        catch (Exception e)
        {
            errorText.GetComponentInChildren<TMP_Text>().text = e.Message;
            errorText.SetActive(true);
        }
    }

    public static Assembly Compile(string source)
    {
        // Replace this Compiler.CSharpCodeProvider wth aeroson's version
        // if  targeting non-Windows platforms:
        var provider = new CSharpCodeProvider();
        var param = new CompilerParameters();

        // Add specific assembly references
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // could optionally add 'assembly.FullName.Contains("System")' below to include common types like colletions
            // But investigate everything added because of potential mallicious classes. Otherwise this works well
            if (assembly.FullName.Contains("UnityEngine") || assembly.FullName.Contains("Assembly-CSharp") || assembly.FullName.Contains("netstandard"))
            {
                param.ReferencedAssemblies.Add(assembly.Location);
            }
        }

        

        // System namespace for common types like collections.
        //param.ReferencedAssemblies.Add("System.dll");

        // This contains methods from the Unity namespaces:
        //param.ReferencedAssemblies.Add("UnityEngines.dll");

        // This assembly contains runtime C# code from Assets folders:
        // (If  using editor scripts, they may be in another assembly)
        //param.ReferencedAssemblies.Add("CSharp.dll");


        // Generate a dll in memory
        param.GenerateExecutable = false;
        param.GenerateInMemory = true;

        // Compile the source
        var result = provider.CompileAssemblyFromSource(param, source);

        if (result.Errors.Count > 0)
        {
            var msg = new StringBuilder();
            foreach (CompilerError error in result.Errors)
            {
                msg.AppendFormat("Error ({0}): {1}\n",
                    error.ErrorNumber, error.ErrorText);
            }
            throw new Exception(msg.ToString());
        }

        // Return the assembly
        return result.CompiledAssembly;
    }

    public void Stopped()
    {
        Debug.Log("Code Done!");
        running = false;
        step = -1;
    }

    public void ForceStop()
    {
        StopCoroutine(coroutine);
        Stopped();
    }

    public int GetStep()
    { 
        return step; 
    }

    public bool GetStepping()
    {
        return stepping;
    }

    public void SetTankObj(GameObject tankobj)
    {
        this.tankobj = tankobj;
    }
}
