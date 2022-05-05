﻿namespace System.IO.Ports
{
	using System;

	public static partial class RxSerialPort
	{
		public static IObserver<string> CreateWriteLineObserver(
			string portName,
			Action<Exception> errorAction = null,
			Action completedAction = null)
		{
			if (string.IsNullOrWhiteSpace(portName))
			{
				throw new ArgumentException($"'{nameof(portName)}' cannot be null or whitespace.", nameof(portName));
			}

			return CreateWriteLineObserver(() => new SerialPort(portName), errorAction, completedAction);
		}

		public static IObserver<string> CreateWriteLineObserver(
			Func<SerialPort> portFactory,
			Action<Exception> errorAction = null,
			Action completedAction = null)
		{
			if (portFactory is null)
			{
				throw new ArgumentNullException(nameof(portFactory));
			}

			return CreateObserver(
				portFactory,
				(serialPort, line) => serialPort.WriteLine(line),
				errorAction,
				completedAction);
		}
	}
}
