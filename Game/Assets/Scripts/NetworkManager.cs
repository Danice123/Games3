using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	private float btnX, btnY, btnW, btnH;
	private string gameName = "edu.gcc.wb.2DMOBA";  // You need to change this for your game!
	private bool refreshingHostList = false;
	private bool hostDataFound = false;
	private HostData[] hostData;
	GUIStyle customButtonStyle;
	public bool useAWSserver = false;
	public string AWS_URL;

	public GameObject charScreen;

	// Use this for initialization
	void Start ()
	{
		samples = new float[30];
		for (int i = 0; i < 30; i++)
			samples [i] = 0f;
		btnX = Screen.width * 0.01f;
		btnY = Screen.width * 0.01f;
		btnW = Screen.width * 0.15f;
		btnH = Screen.width * 0.15f;

		//Modify the Master server and facilitator attributes to connect to my locally
		//hosted server instead of the Unity HQ Master Server
		if (useAWSserver) {
				//Use the following four lines if using the class AWS Master Server
				MasterServer.ipAddress = AWS_URL; //"54.187.184.133"; //"10.37.101.31";
				MasterServer.port = 23466;
				Network.natFacilitatorIP = AWS_URL; //"54.187.184.133"; //"10.37.101.31";
				Network.natFacilitatorPort = 50005;
		}
	}

	//Host a server and register it to the master server
	void startServer ()
	{
		Network.InitializeServer (2, 25001, !Network.HavePublicAddress ());
		MasterServer.RegisterHost (gameName, "Mine!", "Testing  stuff");
	}

	//Get the list of servers from the Master Server
	void refreshHostList ()
	{
		MasterServer.RequestHostList (gameName);
		refreshingHostList = true;
		Debug.Log ("Getting Host List");
	}

	//Messages
	void OnServerInitialized ()
	{
		Debug.Log ("Server initialized");
		charScreen.SetActive (true);
	}

	void OnConnectedToServer ()
	{
		Debug.Log ("Connected to server");
		charScreen.SetActive (true);
	}

	void OnMasterServerEvent (MasterServerEvent mse)
	{
		if (mse == MasterServerEvent.RegistrationSucceeded) {
				Debug.Log ("Server registered");
		}
	}

	//Update functions for GUI and Per-frame update
	void OnGUI ()
	{
		if (customButtonStyle == null) {
				customButtonStyle = new GUIStyle (GUI.skin.button);
				customButtonStyle.fontSize = 15;
		}
		if (!Network.isClient && !Network.isServer) {
				GUILayout.BeginArea (new Rect (Screen.width * .05f, Screen.height * .05f, Screen.width * 0.1f, Screen.height * 0.5f));
				if (GUILayout.Button ("Start Server", customButtonStyle)) {
						Debug.Log ("Starting Server");
						startServer (); 
				}
	
				if (GUILayout.Button ("Refresh Host List", customButtonStyle)) {
						Debug.Log ("Refreshing...");
						refreshHostList ();
				}
	
				if (hostDataFound) {
						Debug.Log ("Host data recieved");
						for (int i=0; i<hostData.Length; i++) {
								if (GUILayout.Button (hostData [i].gameName, customButtonStyle)) {
										Network.Connect (hostData [i]);
								}
						}
				}
				GUILayout.EndArea ();
		}
	}

	void Update ()
	{
		//If we have started to look for available servers, look every frame until we find one.
		if (refreshingHostList) {
				if (MasterServer.PollHostList ().Length > 0) {
						refreshingHostList = false;
						hostDataFound = true;
						hostData = MasterServer.PollHostList ();
				}
		}
	}

	int ticks = 0;
	float timer = 0;
	bool timerRunning = false;

	public GameObject latencyCounter;
	float[] samples;
	int iSamples = 0;
	float sum;
	bool full = false;

	void FixedUpdate() {
		if (Network.isServer) {
			if (ticks >= 30) {
				ticks = 0;
				GetComponent<NetworkView> ().RPC ("latencyCheck", RPCMode.Others, null);
				timerRunning = true;
			}
			if (timerRunning)
				timer += Time.deltaTime;
			ticks++;
		}
	}

	[RPC]
	void latencyCheck() {
		GetComponent<NetworkView> ().RPC ("latencyReturn", RPCMode.Others, null);
	}

	[RPC]
	void latencyReturn() {
		timerRunning = false;
		samples [iSamples] = timer;
		int previous = (iSamples + 30 - 1) % 30;
		sum = sum + timer - samples [previous];
		if (iSamples == 30)
			full = true;
		iSamples = (iSamples + 1) % 30;
		if (full) {
			Debug.Log (sum / 30f * 1000f + " ms");
		} else {
			Debug.Log (sum / iSamples * 1000f + " ms");
		}
		timer = 0;
	}


}


