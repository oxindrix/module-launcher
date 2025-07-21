using System;


namespace Services.SignalBusSystem
{
	public interface ISignalBus
	{
		public void Subscribe<T>(Action<T> action) where T : class;
		public void UnSubscribe<T>(Action<T> action) where T : class;

		public void Publish<T>(T message) where T : class;
	}
}