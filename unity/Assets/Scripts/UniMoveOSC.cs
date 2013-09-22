using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UniMoveOSC : Manager<UniMoveOSC> {
	public string OSCHost = "127.0.0.1";
	public int SendToPort = 7773;
	public int ListenerPort = 7772;
	public Plotter plotter;
	private Osc _handler;
	
	void Awake () {
		UDPPacketIO udp = GetComponent<UDPPacketIO>();
		udp.init(OSCHost, SendToPort, ListenerPort);
		_handler = GetComponent<Osc>();
		_handler.init(udp);
		
		_handler.SetAddressHandler("/test", TestMessage);
	}
	
	// ==========
	// Messages Handling
	// ==========
	private void TestMessage(OscMessage oscMessage) {
		int[] result = new int[1];
		result[0] = oscMessageToInt(oscMessage);
		plotter.Stream(result);
	}
	
	
	// ======================
	// OSC Message processing
	// ======================
	private int oscMessageToInt(OscMessage oscMessage) {
		return int.Parse(oscMessage.Values[0] + "");
	}
	
	private float oscMessageToFloat(OscMessage oscMessage) {
		return float.Parse(oscMessage.Values[0] +"");
	}
	
	private int[] oscMessageToIntArray(OscMessage oscMessage) {
		string[] split = (oscMessage.Values[0] +"").Trim().Split(" "[0]);
		int[] result = new int[split.Length];
		
		for (var i = 0; i < split.Length; ++i) {
			result[i] = int.Parse(split[i]);
		}
		return result;
	}
	
	public void Send(string id, string content) {
		_handler.Send(Osc.StringToOscMessage("/" + id + " " + content));	
	}
}
