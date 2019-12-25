using Infrastructure;
using Infrastructure.Enums;
using Infrastructure.Packets.Message;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    List<Message> messageList = new List<Message>();
    private readonly int maxMessages = 25;

    public GameObject TextObject, ChatPanel;
    public InputField input;

    [SerializeField]
    private NetworkManager networkManager;

    private void Awake()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            input.ActivateInputField();
            input.Select();
        }
    }

    private void inputSubmitCallBack()
    {
        if (input.text != "")
        {
            var msg = new CMSG_Message { Message = input.text };
            networkManager.Send(msg.Serialize());

            input.text = ""; //Clear Inputfield text
            input.DeactivateInputField(); //Re-focus on the input field
        }
    }

    void OnEnable()
    {
        //Register InputField Events
        input.onEndEdit.AddListener(delegate { inputSubmitCallBack(); });
    }

    void OnDisable()
    {
        //Un-Register InputField Events
        input.onEndEdit.RemoveAllListeners();
        input.onValueChanged.RemoveAllListeners();
    }

    private void SendMessageToChat(string text)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        var message = new Message { text = text };

        GameObject newText = Instantiate(TextObject, ChatPanel.transform);
        message.textObject = newText.GetComponent<Text>();
        message.textObject.text = message.text;

        messageList.Add(message);
    }

    public void MessageReceived(SMSG_Message message)
    {
        switch (message.MessageType)
        {
            case MessageType.UserMessage:
                SendMessageToChat($"[{message.GetTimestamp().ToString("hh:mm:ss")}]{message.UserName}:{message.Message}");
                break;
            default:
                SendMessageToChat($"[{message.GetTimestamp()}]DEFAULT NOT IMPLEMENTED:{message.Message}");
                break;
        }
    }
}

public class Message
{
    public string text;
    public Text textObject;
}