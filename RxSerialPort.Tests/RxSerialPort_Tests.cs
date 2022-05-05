﻿namespace System.IO.Ports.Tests
{
	using System.Reactive.Linq;
	using System.Threading.Tasks;
	using Xunit;

	public class RxSerialPort_Tests
	{
		[Fact()]
		public async Task Connect_Test()
		{
			var serialPort = new SerialPort(SerialPort.GetPortNames()[0]);
			var serialPort2 = new SerialPort(SerialPort.GetPortNames()[1]);

			serialPort.Connect()
				.Where(@event => @event.EventType == SerialPortEventType.DataReceived)
				.Select(@event => @event.Data)
				.Subscribe(data => { }, ex => { }, () => { });

			serialPort.Open();
			serialPort2.Open();
			serialPort2.WriteLine("Hello Port");

			await Task.Delay(500);

			serialPort2.Dispose();
			serialPort.Dispose();
		}

		[Fact()]
		public async Task ConnectByName()
		{
			var serialPort2 = new SerialPort(SerialPort.GetPortNames()[1]);

			var sub = RxSerialPort.Connect(SerialPort.GetPortNames()[0])
				.Where(@event => @event.EventType == SerialPortEventType.DataReceived)
				.Select(@event => @event.Data)
				.Subscribe(data => { }, ex => { }, () => { });

			serialPort2.Open();
			serialPort2.WriteLine("Hello Port");

			await Task.Delay(500);

			serialPort2.Dispose();
			sub.Dispose();
		}
	}
}