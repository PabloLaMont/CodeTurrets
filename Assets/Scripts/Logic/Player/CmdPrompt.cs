using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CmdPrompt : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button enterButton;
    public ScrollViewSpawner spawner;
    public ObjectPool codeLinePool;

    public List<CPUTurret> cpus;
    public List<GpuTurret> gpus;

    private IEnumerator Start()
    {
        yield return null;
        enterButton.onClick.AddListener(CompareInput);
        inputField.onEndEdit.AddListener(delegate { if (Input.GetKeyDown(KeyCode.Return)) CompareInput(); });

        cpus = new List<CPUTurret>(FindObjectsOfType<CPUTurret>());
        gpus = new List<GpuTurret>(FindObjectsOfType<GpuTurret>());
    }

    private void CompareInput()
    {
        string input = inputField.text;

        if (input.StartsWith("GPU"))
        {
            input = input.Substring(3).Trim();  
            foreach (var gpu in gpus)
            {
                if (!gpu.gameObject.activeInHierarchy || gpu.IsCompiling)
                {
                    continue;
                }

                foreach (var item in spawner.currentPool)
                {
                    CodeLine codeLine = item.GetComponent<CodeLine>();
                    if (codeLine != null)
                    {
                        foreach (string answer in codeLine.Answers)
                        {
                            if (input.Contains(answer))
                            {
                                StartCoroutine(gpu.Compile(codeLine, item));
                                inputField.text = "";
                                return;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            foreach (var cpu in cpus)
            {
                if (!cpu.gameObject.activeInHierarchy || cpu.IsCompiling)
                {
                    continue;
                }

                foreach (var item in spawner.currentPool)
                {
                    CodeLine codeLine = item.GetComponent<CodeLine>();
                    if (codeLine != null)
                    {
                        foreach (string answer in codeLine.Answers)
                        {
                            if (input.Contains(answer))
                            {
                                StartCoroutine(cpu.Compile(codeLine, item));
                                inputField.text = "";
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}