using Modules.CatLady.DTO.Signals;
using Services.SignalBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;


namespace Modules.CatLady.UI
{
	public class Hud : MonoBehaviour
	{
		[SerializeField] private TMP_Text textStartInfo;
		[SerializeField] private TMP_Text textGameScore;
		[SerializeField] private TMP_Text textFinalScore;
		[SerializeField] private TMP_Text textFinalMessage;
		[SerializeField] private Button buttonRestart;

		private ISignalBus signalBus;
		
		
		[Inject]
		public void Construct(ISignalBus signalBus)
		{
			this.signalBus = signalBus;
		}
		

		private void Start()
		{
			buttonRestart.onClick.AddListener(() => signalBus.Publish(new OnRestartRequested()));
		}


		public void SetScore(int score)
		{
			textGameScore.text = score.ToString();
			textFinalScore.text = textGameScore.text;
		}


		public void SetGameScoreVisible(bool value) =>
			textGameScore.gameObject.SetActive(value);


		public void SetFinalScoreVisible(bool value) =>
			textFinalScore.gameObject.SetActive(value);


		public void SetStartInfoVisible(bool value) =>
			textStartInfo.gameObject.SetActive(value);


		public void SetFinalMessageVisible(bool value) =>
			textFinalMessage.gameObject.SetActive(value);


		public void SetFinalMessage(string message) => 
			textFinalMessage.text = message;


		public void SetRestartButtonVisible(bool value) =>
			buttonRestart.gameObject.SetActive(value);
	}
}