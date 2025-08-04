using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class ClickableCamera : MonoBehaviour
{
    [System.Serializable]
    public class OpenAIResponse
    {
        public List<Choice> choices;
    }

    [System.Serializable]
    public class Choice
    {
        public Message message;
    }

    [System.Serializable]
    public class Message
    {
        public string content;
    }

    public Text detectionText;

    public TMP_Text infoTitle;
    public TMP_Text infoContent;

    public TMP_Text chatGPTResponse;
    public TMP_InputField chatGPTRequest;

    private string currentContext = "Computer";

    public GameObject GPUObject;
    public GameObject RAMObject;
    public GameObject CPUObject;
    public GameObject StorageObject;
    public GameObject MotherboardObject;

    public GameObject ComputerObject;
    public GameObject TopLaptopObject;
    public GameObject BottomLaptopObject;

    public GameObject InfoPanel;
    public GameObject AIPanel;

    private string openAIApiKey = "HIDDEN";
    private static readonly HttpClient httpClient = new HttpClient();

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Works for mouse clicks and Android touches
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // or use Input.GetTouch(0).position for mobile
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.transform.CompareTag("Keyboard")) // Tag your keyboard object properly
                {
                    infoTitle.text = "Keyboard";
                    infoContent.text = "A primary input device used for typing and issuing commands. Contains keys for letters, numbers, and functions.";
                }

                if (hit.transform.CompareTag("Case")) // Tag your keyboard object properly
                {

                    MotherboardObject.SetActive(true);

                    StartCoroutine(ShowAfterMotherBoard());

                }

                if (hit.transform.CompareTag("Computer")) // Tag your keyboard object properly
                {

                    TopLaptopObject.SetActive(true);
                    BottomLaptopObject.SetActive(true);

                    ComputerObject.SetActive(false);

                }


                if (hit.transform.CompareTag("MousePad")) // Tag your keyboard object properly
                {
                    infoTitle.text = "Mousepad";
                    infoContent.text = "A built-in pointing device used to control the cursor and interact with the screen without an external mouse.";
                }

                if (hit.transform.CompareTag("Motherboard")) // Tag your keyboard object properly
                {
                    infoTitle.text = "Motherboard";
                    infoContent.text = "The main circuit board that connects and communicates with all other hardware components like CPU, RAM, and GPU.";
                }

                if (hit.transform.CompareTag("RAM")) // Tag your keyboard object properly
                {
                    infoTitle.text = "Random Access Memory (RAM)";
                    infoContent.text = "A short-term memory that temporarily stores data your computer is currently using for quick access and performance.";
                }

                if (hit.transform.CompareTag("CPU")) // Tag your keyboard object properly
                {
                    infoTitle.text = "Central Processing Unit (CPU)";
                    infoContent.text = "The “brain” of the computer, responsible for executing instructions and performing calculations.";
                }

                if (hit.transform.CompareTag("GPU")) // Tag your keyboard object properly
                {
                    infoTitle.text = "Graphical Processing Unit (GPU)";
                    infoContent.text = "Handles image rendering, video processing, and gaming performance. Crucial for visual computing.";
                }

                if (hit.transform.CompareTag("Storage")) // Tag your keyboard object properly
                {
                    infoTitle.text = "Storage (HDD/SSD)";
                    infoContent.text = "Where all files, programs, and the operating system are stored permanently, even when the laptop is off.";
                }

                if (hit.transform.CompareTag("Screen")) // Tag your keyboard object properly
                {
                    infoTitle.text = "Screen (Display)";
                    infoContent.text = "The main output interface of the laptop. Displays images, videos, programs, and everything you interact with. Modern screens are typically high-resolution and may include touchscreen features.";
                }
            }
        }
    }

    IEnumerator ShowAfterMotherBoard()
    {
        yield return new WaitForSeconds(3f); // 3 seconds
        GPUObject.SetActive(true);
        RAMObject.SetActive(true);
        CPUObject.SetActive(true);
        StorageObject.SetActive(true);
    }
    public void Test()
    {

        detectionText.text = "chatGPTRequest = " + (chatGPTRequest == null ? "❌ NULL" : "✅ OK") +
            "chatGPTRequest = " + (chatGPTRequest == null ? "❌ NULL" : "✅ OK") + "chatGPTResponse = " + (chatGPTResponse == null ? "❌ NULL" : "✅ OK") + "infoTitle = " + (infoTitle == null ? "❌ NULL" : "✅ OK") + "infoContent = " + (infoContent == null ? "❌ NULL" : "✅ OK");


        //detectionText.text = "Detected ✅ ";
        Debug.Log("Keyboard clicked! ✅");
        // Add any other functionality here
    }

    public void HideUI()
    {
        InfoPanel.SetActive(false);
        AIPanel.SetActive(false);
    }

    public void ShowUI()
    {
        InfoPanel.SetActive(true);
        AIPanel.SetActive(true);
    }
    public void AskGPT()
    {
        string userInput = chatGPTRequest.text;
        _ = SendChatGPTRequestAsync(userInput); // Fire and forget async
    }

    private async Task SendChatGPTRequestAsync(string prompt)
    {
        string apiUrl = "https://api.openai.com/v1/chat/completions";

        currentContext = infoTitle.text + ": " + infoContent.text;
        Debug.Log("Context: " + currentContext);

        var requestData = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
            new { role = "system", content = "You are a Computer Tech Information Assistant in an app. the app is called TechLearnAR, the user asks you about " + currentContext + " in computer parts, please answer clearly, educational-focused, simply, and briefly. Do not exceed 5 sentences in your responses." },
            new { role = "user", content = prompt }
        },
            max_tokens = 100,
            temperature = 0.7f
        };

        string jsonData = JsonConvert.SerializeObject(requestData);
        Debug.Log("Request JSON: " + jsonData);

        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + openAIApiKey);

        try
        {
            HttpResponseMessage response = await httpClient.PostAsync(
                apiUrl,
                new StringContent(jsonData, Encoding.UTF8, "application/json")
            );

            string result = await response.Content.ReadAsStringAsync();
            Debug.Log("Raw API Response: " + result);

            if (response.IsSuccessStatusCode)
            {
                // Use JsonUtility instead of JsonConvert for Android compatibility
                OpenAIResponse apiResponse = JsonUtility.FromJson<OpenAIResponse>(result);

                if (apiResponse?.choices != null && apiResponse.choices.Count > 0)
                {
                    string reply = apiResponse.choices[0].message.content;
                    Debug.Log("Parsed Reply: " + reply);

                    // Ensure UI updates happen on main thread
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        chatGPTResponse.text = reply;
                    });
                }
                else
                {
                    throw new Exception("Invalid API response structure");
                }
            }
            else
            {
                throw new Exception($"API Error {response.StatusCode}: {result}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Full Error: " + ex.ToString());

            UnityMainThreadDispatcher.Instance().Enqueue(() => {
                chatGPTResponse.text = $"Error: {ex.Message}";
            });
        }
    }

}
